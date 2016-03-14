using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AnimationImageAnalogy
{
    public class ImageAnalogy
    {
        /* Source pair */
        private Color[,] imageA1;
        private Color[,] imageA2;

        /* The pixel dimension of the patches we are iterating by */
        private int patchDimension;

        /* Determines number of pixels we iterate by each time */
        private int patchIter;

        /* Constructor */
        public ImageAnalogy(Color[,] imageA1, Color[,] imageA2, int patchDimension, int patchIter) 
        {
            this.imageA1 = imageA1;
            this.imageA2 = imageA2;
            this.patchDimension = patchDimension;
            this.patchIter = patchIter;
        }

        /* Compute patch value
         * patchLocA is the top left corner of the patch in A
         * patchLocB is the top left corner of the patch in B
         */
        private int ComputePatchValue(Color[,] imageA, Color[,] imageB, 
            Tuple <int,int> patchLocA, int bX, int bY, int patchXSize, int patchYSize)
        {
            int patchValue = 0;
            for (int i = 0; i < patchXSize; i++) 
            {
                for (int j = 0; j < patchYSize; j++)
                {
                    if( patchLocA.Item1 + i >= imageA.GetLength(0) || patchLocA.Item2 + j >= imageA.GetLength(1))
                    {
                        break;
                    }


                    //Compute sum of squared differences for each pixel in patch
                    Color a = imageA[patchLocA.Item1+i, patchLocA.Item2+j];
                    Color b = imageB[bX+i, bY+j];

                    int ssd = (int)( Math.Pow((a.A - b.A), 2) + Math.Pow((a.R - b.R), 2)
                        + Math.Pow((a.G - b.G), 2) + Math.Pow((a.B - b.B), 2));

                    //Add together all of the ssd's into one final value representing the patch
                    patchValue += ssd;

                }
            }
            return patchValue;
        }

        /* Returns pixel at the top left corner of the patch in A1 which is the closest match
         * to the current patch in B1. */
        private Tuple<int,int> BestPatchMatch(Color[,] imageB1, int bX, int bY)
        {
            int width = imageA1.GetLength(0);
            int height = imageA1.GetLength(1);

            //Keeps track of coordinates and values for the best patch we've found so far
            Tuple<int, int> bestPatchA1 = null;
            //we are looking for the smallest patchval, so set to max int as default
            int bestPatchVal = Int32.MaxValue; 

            //Keeps track of the current patch value in the patch we're iterating on
            int currentPatchVal = 0;

            //Iterate through A1 to find the closest match for this patch in B1
            //for (int i = 0; i < width; i += patchIter)
            for (int i = 0; i < width; i++)
            {
                //for (int j = 0; j < height; j += patchIter)
                for (int j = 0; j < height; j++)
                {
                    //Make sure that it is a full patch
                    if (width - i > patchDimension && height - j > patchDimension)
                    {
                        Tuple<int, int> currentPatchA1 = new Tuple<int, int>(i, j);
                        currentPatchVal = ComputePatchValue(imageA1, imageB1, currentPatchA1, bX, bY,
                            patchDimension, patchDimension);

                        if (currentPatchVal < bestPatchVal)
                        {
                            bestPatchVal = currentPatchVal;
                            bestPatchA1 = currentPatchA1;
                        }
                    }
                    
                }
            }

            return bestPatchA1;
        }

        /* Find the best patch among n randomly selected patches. */
        private Tuple<int, int> BestRandomPatch(Color[,] imageB1, int bX, int bY, int n)
        {
            int width = imageA1.GetLength(0);
            int height = imageA1.GetLength(1);

            //Keeps track of coordinates and values for the best patch we've found so far
            Tuple<int, int> bestPatchA1 = null;
            //we are looking for the smallest patchval, so set to max int as default
            int bestPatchVal = Int32.MaxValue;

            //Keeps track of the current patch value in the patch we're iterating on
            int currentPatchVal = 0;

            Random r = new Random();

            //Test n randomly selected patches for the best match
            for(int i = 0; i < n; i++)
            {
                //Select the top left coordinates of the random patch
                int xRand = r.Next(0, width);
                int yRand = r.Next(0, height);

                //Make sure that it is a full patch
                if (width - xRand > patchDimension + 1 && height - yRand > patchDimension + 1)
                {
                    Tuple<int, int> currentPatchA1 = new Tuple<int, int>(xRand, yRand);
                    currentPatchVal = ComputePatchValue(imageA1, imageB1, currentPatchA1, bX, bY,
                        patchDimension, patchDimension);

                    if (currentPatchVal < bestPatchVal)
                    {
                        bestPatchVal = currentPatchVal;
                        bestPatchA1 = currentPatchA1;
                    }
                }
            }

            return bestPatchA1;
        }

        /* Copies the patch with top left corner at indices designated by patchA from imageA2 to 
         * the patch with top left corner at indices bX, bY in imageB2. The patches should overlap,
         * so compute Dijkstra's algorithm to resolve the overlap.
         */
        private Color[,] copyPatch(Color[,] imageA2, Color[,] imageB2, Tuple<int,int> patchA, int bX, int bY)
        {
            /* Calculate bounds of patch */
            int beginX = patchA.Item1;
            int endX = beginX + patchDimension;
            int beginY = patchA.Item2;
            int endY = beginY + patchDimension;

            int currentBX = bX;
            int currentBY = bY;

            for(int i = beginX; i < endX; i++)
            {
                currentBY = bY;
                for (int j = beginY; j < endY; j++)
                {
                    imageB2[currentBX, currentBY] = imageA2[i, j];
                    currentBY++;
                }
                currentBX++;
            }
            return imageB2;
        }

        private Color[,] copyPatchOnlyRight(Color[,] imageA2, Color[,] imageB2, Tuple<int,int> patchA, int bX, int bY)
        {
            /* Calculate bounds of patch */
            int overlapSize = patchDimension - patchIter;


            int beginX = patchA.Item1 + overlapSize;
            int endX = beginX + patchIter;
            int beginY = patchA.Item2 + overlapSize;
            int endY = beginY + patchIter;

            int currentBX = bX;
            int currentBY = bY;

            if (endY >= imageA2.GetLength(1) || endX >= imageA2.GetLength(0))
            {
                return imageB2;
            }

            for (int i = beginX; i < endX; i++)
            {
                currentBY = bY;
                for (int j = beginY; j < endY; j++)
                {
                    imageB2[currentBX, currentBY] = imageA2[i, j];
                    currentBY++;
                }
                currentBX++;
            }
            return imageB2;
        }

        /* Copies patch by performing average in the overlap */
        private Color[,] copyPatchAverage(Color[,] imageA2, Color[,] imageB2, Tuple<int,int> patchA, int bX, int bY)
        {
            /* Calculate bounds of overlap */
            int beginX = patchA.Item1 + patchIter - 1;
            int endX = beginX + patchDimension - 1;
            int beginY = patchA.Item2 + patchIter - 1;
            int endY = beginY + patchDimension - 1;

            int currentBX = bX;
            int currentBY = bY;

            if(endY >= imageA2.GetLength(1) || endX >= imageA2.GetLength(0))
            {
                return imageB2;
            }

            for(int i = beginX; i < endX; i++)
            {
                currentBY = bY;
                for (int j = beginY; j < endY; j++)
                {
                    Color a2 = imageA2[i, j];
                    Color b2 = imageB2[currentBX, currentBY];
                    Color avgColor = Color.FromArgb((a2.A + b2.A)/2, (a2.R + b2.R)/2, (a2.G + b2.G)/2, (a2.B+b2.B)/2);

                    imageB2[currentBX, currentBY] = avgColor;
                    currentBY++;
                }
                currentBX++;
            }
            
            
            return imageB2;
        }

        //bX and bY are the top left corner of the patch
        private Color[,] copyPatchDijkstra(Color[,] imageA2, Color[,] imageB2, Tuple<int,int> patchA, int bX, int bY, bool horizontal)
        {
            /* Calculate bounds of patch */
            int beginX = patchA.Item1;
            int endX = beginX + patchDimension;
            int beginY = patchA.Item2;
            int endY = beginY + patchDimension;

            //Initialize the patch graph in preparation for finding the shortest path
            PatchGraph pg = new PatchGraph(patchDimension, patchIter);
            pg.createGraph(imageB2, imageA2, new Tuple<int, int>(bX, bY), patchA, horizontal);
            pg.initializeGraphNeighborsWeights(beginX, endX, beginY, endY, horizontal);

            //Find the shortest path using dijkstra's for the x overlap
            //TODO: Right now it finds shortest path between the two midpoints, but
            //we should actually do it between the smallest value in the rows
            int mid;
            Node start;
            Node end;

            if (horizontal)
            {
                mid = pg.graph.GetLength(0) / 2;
                start = pg.graph[mid, 0];
                end = pg.graph[mid, pg.graph.GetLength(1)-1];
            } else
            {
                mid = pg.graph.GetLength(1) / 2;
                start = pg.graph[0, mid];
                end = pg.graph[pg.graph.GetLength(0)-1, mid];
            }

            pg.findShortestPath(start, end, horizontal);
            Queue<Tuple<int,int>> shortestPath = pg.shortestPath;
            
            //Loop through the patches. Depending on which side of the
            //path the pixel is on, choose to keep either the color value
            //from A or from B. Pixels on the path will be taken from B.
            int currentBX = bX;
            int currentBY = bY;
            Tuple<int,int> pathNode = shortestPath.Dequeue();

            //for (int i = beginX; i < endX; i++)
            for(int j = beginY; j < endY; j++)
            {
                //Console.WriteLine("PATHNODE: " + pathNode);
                currentBY = bY;
                for (int i = beginX; i < endX; i++)
                //for (int j = beginY; j < endY; j++)
                {
                    if(horizontal)
                    {
                        //Console.WriteLine("i:" + i);
                        //Console.WriteLine("j:" + j);
                        //Console.WriteLine("pathNode" + pathNode);
                        if (i - beginX > pathNode.Item1)
                        {
                            //Console.WriteLine("right side of path");
                            //If we're on the right side of the path, put in values from imageA2
                            imageB2[currentBX, currentBY] = imageA2[i, j];
                            //Console.WriteLine("YO SUP!!!!!!!!!!!!!");
                        } else
                        {
                           //imageB2[currentBX, currentBY] = Color.Aqua;
                           //Console.WriteLine("left side of path");
                            //If we're on the left side of the path, leave it as is
                            //Console.WriteLine("hello?");
                        }
                    } else
                    {
                        if (j - beginY > pathNode.Item2)
                        {
                            imageB2[currentBX, currentBY] = imageA2[i, j];
                            break;
                            //Console.WriteLine("below the path");
                            //We're below the path, so put in values from imageA2
                            if (j < beginY + patchDimension / 2)
                            {
                                //Do a 50-50 blend in this area where the dijkstra's overlap
                                Color current = imageB2[currentBX, currentBY];
                                Color aColor = imageA2[i, j];
                                int aVal = (current.A + aColor.A) / 2;
                                int rVal = (current.R + aColor.R) / 2;
                                int gVal = (current.G + aColor.G) / 2;
                                int bVal = (current.B + aColor.B) / 2;
                                Color average = Color.FromArgb(aVal, rVal, gVal, bVal);
                                imageB2[currentBX, currentBY] = average;
                            } else
                            {
                                if (i >= beginX + patchDimension / 2)
                                {
                                    imageB2[currentBX, currentBY] = imageA2[i, j];
                                }
                            }
                            
                        } else
                        {
                            imageB2[currentBX, currentBY] = Color.Aqua;
                            break;
                            //Console.WriteLine("above the path");
                            if (j < beginY + patchDimension / 2)
                            {
                                //Do a 50-50 blend in this area where the dijkstra's overlap
                                Color current = imageB2[currentBX, currentBY];
                                Color aColor = imageA2[i, j];
                                int aVal = (current.A + aColor.A)/ 2;
                                int rVal = (current.R + aColor.R) / 2;
                                int gVal = (current.G + aColor.G) / 2;
                                int bVal = (current.B + aColor.B)/ 2;
                                Color average = Color.FromArgb(aVal, rVal, gVal, bVal);
                                imageB2[currentBX, currentBY] = average;
                            }
                            //Console.WriteLine("hello2?");
                        }
                    }
                    
                    //otherwise leave imageB2 as it is
                    currentBY++;
                }
                currentBX++;

                if(horizontal)
                {
                    int currentNodeY = pathNode.Item2;
                    if (currentNodeY != patchDimension - 1)
                    {
                        while (pathNode.Item2 == currentNodeY)
                        {
                            pathNode = shortestPath.Dequeue();
                        }
                        //Console.WriteLine("pathNode:" + pathNode);
                    }
                }
                else
                {
                    int currentNodeX = pathNode.Item1;
                    if (currentNodeX != patchDimension - 1)
                    {
                        while (pathNode.Item1 == currentNodeX)
                        {
                            pathNode = shortestPath.Dequeue();
                        }
                        //Console.WriteLine(pathNode.Item2);
                    }
                }
            }
            //Console.WriteLine("PATCH ENDED!!!!!!");
            return imageB2;
        }

        private Color[,] copyPatchDijkstraNew(Color[,] imageA2, Color[,] imageB2, Tuple<int, int> patchA, Tuple<int,int> patchB, bool horizontal)
        {
            /* Calculate bounds of patch */
            int beginX = patchA.Item1;
            int endX = beginX + patchDimension;
            int beginY = patchA.Item2;
            int endY = beginY + patchDimension;

            //Initialize the patch graph in preparation for finding the shortest path
            PatchGraph pg = new PatchGraph(patchDimension, patchIter);
            pg.createGraph(imageB2, imageA2, patchB, patchA, horizontal);
            pg.initializeGraphNeighborsWeights(beginX, endX, beginY, endY, horizontal);

            //Find the shortest path using dijkstra's
            //TODO: Right now it finds shortest path between the two midpoints, but
            //we should actually do it between the smallest value in the rows
            int mid;
            Node start;
            Node end;

            if (horizontal)
            {
                //We're finding a horizontal line
                mid = pg.graph.GetLength(1) / 2;
                start = pg.graph[0, mid];
                end = pg.graph[pg.graph.GetLength(0) - 1, mid];
            }
            else
            {
                //We're finding a vertical line
                mid = pg.graph.GetLength(0) / 2;
                start = pg.graph[mid, 0];
                end = pg.graph[mid, pg.graph.GetLength(1) - 1];
            }

            //Find the shortest path
            pg.findShortestPath(start, end, horizontal);
            int[] shortestPathArray = pg.shortestPathArray;

            //Loop through the patches. Depending on which side of the
            //path the pixel is on, choose to keep either the color value
            //from A or from B. Pixels on the path will be taken from B.
            int bX = patchB.Item1;
            int bY = patchB.Item2;

            for(int x = beginX; x < endX; x++)
            {
                bY = patchB.Item2;
                for(int y = beginY; y < endY; y++)
                {
                    Color newColor;
                    if (horizontal)
                    {
                        //If our path is horizontal (we are tiling vertically)
                        if (y - beginY > shortestPathArray[x - beginX] || patchB.Item2 == 0)
                        {
                            //If we're below the path, put in color from imageA2
                            //Also if we're at the top of the image we want all of A2
                            //imageB2[bX, bY] = imageA2[x, y];
                            newColor = imageA2[x, y];
                        } else
                        {
                            //If we're above or on the path, keep imageB2 as it is
                            //imageB2[bX, bY] = Color.Aqua;
                            newColor = imageB2[bX, bY];
                        }

                        //Do 50-50 blend if we're in an overlapping region
                        //Make sure we don't do this if we're on the leftmost edge of the image
                        if (patchB.Item1 != 0 && x - beginX < patchDimension/2 )
                        {
                            Color average = blendAverage(newColor, imageB2[bX,bY]);
                            imageB2[bX, bY] = average;
                        } else
                        {
                            imageB2[bX, bY] = newColor;
                        }
                    } else
                    {
                        //If our path is vertical (we are tiling horizontally)
                        if (x - beginX > shortestPathArray[y - beginY] || patchB.Item1 == 0)
                        {
                            //We're to the right of the path, put in color from imageA2
                            //Also if we're at the left of the image we want all of B2
                            //imageB2[bX, bY] = imageA2[x, y];
                            newColor = imageA2[x, y];
                        } else
                        {
                            //We're to the left of the path, keep imageB2 as it is
                            //imageB2[bX, bY] = Color.Aqua;
                            newColor = imageB2[bX, bY];
                        }
                        //Do 50-50 blend if we're in an overlapping region
                        //Make sure we don't do this if we're on the leftmost edge of the image
                        /*
                        if (patchB.Item1 != 0 && x - beginX < patchDimension / 2 && y - beginY >= patchDimension / 2)
                        {
                            //Bottom left corner blending
                            Color average = blendAverage(newColor, imageB2[bX, bY]);
                            imageB2[bX, bY] = average;
                        }
                        else if (patchB.Item2 != 0 && x - beginX >= patchDimension / 2 && y - beginY < patchDimension / 2)
                        {
                            //Upper right corner blending
                            Color average = blendAverage(newColor, imageB2[bX, bY]);
                            imageB2[bX, bY] = average;
                        }
                        else
                        {
                            imageB2[bX, bY] = newColor;
                        }*/
                        Color average = blendAverage(newColor, imageB2[bX, bY]);
                        imageB2[bX, bY] = average;
                    }
                    bY++;
                }
                bX++;
            }
            return imageB2;
        }

        /*
         * Do a 50-50 blend of colors
         */
        private Color blendAverage(Color a, Color b)
        {
            //Color current = imageB2[bX, bY];
            //Color aColor = imageA2[aX, aY];
            int aVal = (a.A + b.A) / 2;
            int rVal = (a.R + b.R) / 2;
            int gVal = (a.G + b.G) / 2;
            int bVal = (a.B + b.B) / 2;
            Color average = Color.FromArgb(aVal, rVal, gVal, bVal);
            return average;
        }


        /* 
         * Create an image analogy (output image) for the given image using the source pair.
         *
         * imageB1: The second simple image we want to generate a complex image for
         * patchNum: The number of random patches we iterate through to find the closest match
         */
        public Color[,] CreateImageAnalogy(Color[,] imageB1, int patchNum)
        {
            int width = imageB1.GetLength(0);
            int height = imageB1.GetLength(1);
            Color[,] imageB2 = new Color[width, height];
            Tuple<int, int> bestMatch = null;

            //TESTING TO MAKE SURE DIJKSTRA ACTUALLY WORKS
            int patchIterX = patchIter;
            int patchIterY = patchIter;

            /*Iterate through patches finding a best match for each */
            for (int col = 0; col < width; col += patchIterX)
            {
                //if (col > 20)
                //    break;
                //Make sure we're not out of column range
                //if (col + patchDimension >= width)
                //SOMETHING IS PROBABLY WRONG HERE SINCE IT SHOULD WORK WITH JUST PATCHDIMENSION
                if (col + patchDimension + patchIter >= width)
                {
                    break;
                }
                for (int row = 0; row < height; row += patchIterY)
                {
                    //Make sure we're not out of bounds for y
                    //if(row + patchDimension >= height)
                    if (row + patchDimension + patchIter >= height)
                    {
                        break;
                    }
                    //Find the best random patch from A1 , testing out patchNum amount of random patches
                    bestMatch = BestRandomPatch(imageB1, col,row, patchNum);

                    //Copy the patch, first with horizontal overlap then with vertical overlap
                    Tuple<int,int> currentBPatch = new Tuple<int, int>(col, row);
                    imageB2 = copyPatchDijkstraNew(imageA2, imageB2, bestMatch, currentBPatch, true);
                    imageB2 = copyPatchDijkstraNew(imageA2, imageB2, bestMatch, currentBPatch, false);

                    Console.WriteLine("Current patch index: " + col + ", " + row);   
                }
            }
            return imageB2;
        }
    }
}
