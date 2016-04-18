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

        /* The patch matches we calculated on the previous frame, if they exist. */
        private Tuple<int, int>[,] currentPrevMatches;

        /* The pixel dimension of the patches we are iterating by */
        private int patchDimension;

        /* Determines number of pixels we iterate by each time */
        private int patchIter;

        //Determines how we calculate the weight of a patch
        private int PATCH_WEIGHT = 5;

        //Determines the radius we will look in for coherence matching
        //i.e. x patches in every direction, for a total of (2x)^2
        private int coherenceRadius;

        //Determines the amount of randomness we allow when finding the best match with coherence
        private float RANDOM_PERCENT = 0.25f;

        /* Constructor */
        public ImageAnalogy(Color[,] imageA1, Color[,] imageA2, int patchDimension, int patchIter, int coherenceRadius) 
        {
            this.imageA1 = imageA1;
            this.imageA2 = imageA2;
            this.patchDimension = patchDimension;
            this.patchIter = patchIter;
            this.coherenceRadius = coherenceRadius;
        }

        /* Compute patch value
         * patchLocA is the top left corner of the patch in A
         * patchLocB is the top left corner of the patch in B
         */
        private int ComputePatchValue(Color[,] imageA, Color[,] imageB, 
            Tuple <int,int> patchLocA, Tuple<int,int> patchLocB, int patchXSize, int patchYSize)
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
                    //Color b = imageB[bX+i, bY+j];
                    Color b = imageB[patchLocB.Item1+i, patchLocB.Item2+j];

                    int ssd = (int)( Math.Pow((a.A - b.A), 2) + Math.Pow((a.R - b.R), 2)
                        + Math.Pow((a.G - b.G), 2) + Math.Pow((a.B - b.B), 2));

                    //Add together all of the ssd's into one final value representing the patch
                    patchValue += ssd;

                }
            }
            return patchValue;
        }

        /* Finds the best random patch from imageA2 which matches the current patch in B1. Finding the patch is weighted
         * by the existing patches in B2 as well as the previous patches we found on the previous frame, if it exists.
         */
        private Tuple<int, int> BestRandomPatchNew(Color[,] imageB1, Color[,] imageB2, int bX, int bY, int searchCount)
        {
            int width = imageA1.GetLength(0);
            int height = imageA1.GetLength(1);
            Tuple<int, int> currentB = new Tuple<int, int>(bX, bY);

            Random r = new Random(Guid.NewGuid().GetHashCode());

            int halfPatchSize = patchDimension / 2;

            //Keeps track of coordinates and values for the best patch we've found so far
            Tuple<int, int> bestPatchA1 = null;
            //we are looking for the smallest patchval, so set to max int as default
            int bestPatchVal = Int32.MaxValue;
            //Keeps track of the current patch value in the patch we're iterating on
            int currentPatchVal = 0;

            //Get the patch to the left of the current patch in imageB2
            Tuple<int, int> patchB2Left = null;
            if(bX - patchIter >= 0)
            {
                patchB2Left = new Tuple<int, int>(bX - patchIter, bY);
            }
            //Get the patch to the top of the current patch in imageB2
            Tuple<int, int> patchB2Top = null;
            if(bY - patchIter >= 0)
            {
                patchB2Top = new Tuple<int, int>(bX, bY - patchIter);
            }

            Tuple<int, int> patchA2Left;
            Tuple<int, int> patchA2Top;

            if(currentPrevMatches != null)
            {
                //Console.WriteLine("doing prev match stuff");
                Tuple<int, int> currentPrevMatch = currentPrevMatches[bX, bY];

                Console.WriteLine(currentPrevMatch);

                //Calculate the bounds for the square we are searching in
                int lowerXBound = currentPrevMatch.Item1 - (coherenceRadius * patchDimension);
                int upperXBound = currentPrevMatch.Item1 + (coherenceRadius * patchDimension);
                int lowerYBound = currentPrevMatch.Item2 - (coherenceRadius * patchDimension);
                int upperYBound = currentPrevMatch.Item2 + (coherenceRadius * patchDimension);

                //Make sure the bounds are within range of the image
                if (lowerXBound - patchDimension < 0)
                {
                    lowerXBound = patchIter;
                }

                if(upperXBound + patchDimension >= width)
                {
                    upperXBound = width - patchDimension - 1;
                }

                if(lowerYBound - patchDimension < 0)
                {
                    lowerYBound = patchIter;
                }

                if(upperYBound + patchDimension >= height)
                {
                    upperYBound = height - patchDimension - 1;
                }

                //Console.WriteLine("width: " + width);
                //Console.WriteLine("height: " + height);
                
                //Console.WriteLine("lowerx: " + lowerXBound);
                //Console.WriteLine("upperx: " + upperXBound);
                //Console.WriteLine("lowery: " + lowerYBound);
                //Console.WriteLine("uppery: " + upperYBound);

                //Search the square for the best match
                for (int i = lowerXBound; i <= upperXBound; i += patchIter)
                {
                    for(int j = lowerYBound; j <= upperYBound; j += patchIter)
                    {
                        Tuple<int, int> currentPatchA1 = new Tuple<int, int>(i,j);

                        currentPatchVal = calculateWeightedSSD(imageB1, imageB2, currentB, patchB2Left, patchB2Top, 
                            currentPatchA1, i, j);

                        if (currentPatchVal < bestPatchVal)
                        {
                            bestPatchVal = currentPatchVal;
                            bestPatchA1 = currentPatchA1;
                        }
                    }
                }


            } else
            {
                //Console.WriteLine("doing normal behavior");

                for (int i = 0; i < searchCount; i++)
                {
                    int xRand = r.Next(patchIter, width - patchDimension);
                    int yRand = r.Next(patchIter, height - patchDimension);

                    //if (width - xRand > patchDimension + 1 && height - yRand > patchDimension + 1)

                    /*
                    //Get the patch to the left of the current patch in imageA2
                    patchA2Left = new Tuple<int, int>(xRand - patchIter, yRand);
                    //Get teh patch to the top of the current patch in imageA2
                    patchA2Top = new Tuple<int, int>(xRand, yRand - patchIter);
                    */
                    Tuple<int, int> currentPatchA1 = new Tuple<int, int>(xRand, yRand);

                    currentPatchVal = calculateWeightedSSD(imageB1, imageB2, currentB, patchB2Left, patchB2Top,
                        currentPatchA1, xRand, yRand);

                    //Calculated the weighted SSD
                    /*
                    int a1b1_val = ComputePatchValue(imageA1, imageB1, currentPatchA1, currentB,
                        patchDimension, patchDimension);
                    int a2b2_val_left = 0;
                    if (patchB2Left != null)
                    {
                        a2b2_val_left = ComputePatchValue(imageA2, imageB2, patchA2Left, patchB2Left,
                        patchDimension, patchDimension);
                    }
                    int a2b2_val_top = 0;
                    if(patchB2Top != null)
                    {
                        a2b2_val_top = ComputePatchValue(imageA2, imageB2, patchA2Top, patchB2Top,
                        patchDimension, patchDimension);
                    }

                    currentPatchVal = PATCH_WEIGHT * a1b1_val + a2b2_val_left + a2b2_val_top;
                    */


                    //Calculated the weighted SSD
                    //d = 5 * dist2(a, b) + dist2(apXLeft, bpXLeftT) + dist2(apYBot, bpYBotT);

                    if (currentPatchVal < bestPatchVal)
                    {
                        bestPatchVal = currentPatchVal;
                        bestPatchA1 = currentPatchA1;
                    }
                }
            }

            //Console.WriteLine("Best Patch: " + bestPatchA1.Item1 + " " + bestPatchA1.Item2);
            return bestPatchA1;
        }

        private int calculateWeightedSSD(Color[,] imageB1, Color[,] imageB2, Tuple<int,int> currentB, 
            Tuple<int,int> patchB2Left, Tuple<int,int> patchB2Top, Tuple<int,int> currentPatchA1, int aX, int aY)
        {
            int currentPatchVal;

            //Get the patch to the left of the current patch in imageA2
            Tuple<int,int> patchA2Left = new Tuple<int, int>(aX - patchIter, aY);
            //Get teh patch to the top of the current patch in imageA2
            Tuple<int,int> patchA2Top = new Tuple<int, int>(aX, aY - patchIter);

            //Calculated the weighted SSD
            int a1b1_val = ComputePatchValue(imageA1, imageB1, currentPatchA1, currentB,
                patchDimension, patchDimension);
            int a2b2_val_left = 0;
            if (patchB2Left != null)
            {
                a2b2_val_left = ComputePatchValue(imageA2, imageB2, patchA2Left, patchB2Left,
                patchDimension, patchDimension);
            }
            int a2b2_val_top = 0;
            if (patchB2Top != null)
            {
                a2b2_val_top = ComputePatchValue(imageA2, imageB2, patchA2Top, patchB2Top,
                patchDimension, patchDimension);
            }

            currentPatchVal = PATCH_WEIGHT * a1b1_val + a2b2_val_left + a2b2_val_top;

            return currentPatchVal;
        }

        /* Copies the patch with top left corner at indices designated by patchA from imageA2 to 
         * the patch with top left corner at indices bX, bY in imageB2. The patches should overlap,
         * so compute Dijkstra's algorithm to resolve the overlap.
         *
         * The "simultaneous" refers to the fact that we find horizontal and vertical shortest paths, calculate where
         * they intersect, and use all this information to determine at once what pixel to choose, rather than doing
         * horizontal and vertical in separate steps like we did before.
         */
        private Color[,] copyPatchDijkstraSimultaneous(Color[,] imageA2, Color[,] imageB2, Tuple<int, int> patchA, Tuple<int, int> patchB, bool horizontal)
        {
            int patchOverlap = patchDimension - patchIter;
            /* Calculate bounds of patch */
            int beginX = patchA.Item1;
            int endX = beginX + patchDimension;
            int beginY = patchA.Item2;
            int endY = beginY + patchDimension;

            //Initialize the patch graph in preparation for finding the horizontal shortest path
            PatchGraph pgHorizontal = new PatchGraph(patchDimension, patchIter);
            pgHorizontal.createGraph(imageB2, imageA2, patchB, patchA, true);
            pgHorizontal.initializeGraphNeighborsWeights(beginX, endX, beginY, endY, true);

            //Initialze the patch graph in prep for finding the vertical shortest path
            PatchGraph pgVertical = new PatchGraph(patchDimension, patchIter);
            pgVertical.createGraph(imageB2, imageA2, patchB, patchA, false);
            pgVertical.initializeGraphNeighborsWeights(beginX, endX, beginY, endY, false);

            //Find the shortest path using dijkstra's
            //TODO: Right now it finds shortest path between the two midpoints, but
            //we should actually do it between the smallest value in the rows
            int midH = pgHorizontal.graph.GetLength(1) / 2;
            Node startH = pgHorizontal.graph[0, midH];
            Node endH = pgHorizontal.graph[pgHorizontal.graph.GetLength(0) - 1, midH];
            
            int midV = pgVertical.graph.GetLength(0) / 2;
            Node startV = pgVertical.graph[midV, 0];
            Node endV = pgVertical.graph[midV, pgVertical.graph.GetLength(1) - 1];

            //Find the shortest paths
            pgHorizontal.findShortestPath(startH, endH, true);
            pgVertical.findShortestPath(startV, endV, false);
            int[] shortestPathArrayH = pgHorizontal.shortestPathArray;
            int[] shortestPathArrayV = pgVertical.shortestPathArray;

            //Loop through the patches. Depending on which side of the
            //path the pixel is on, choose to keep either the color value
            //from A or from B. Pixels on the path will be taken from B.
            int bX = patchB.Item1;
            int bY = patchB.Item2;

            for (int x = beginX; x < endX; x++)
            {
                bY = patchB.Item2;
                for (int y = beginY; y < endY; y++)
                {
                    //Color newColor;
                    if(patchB.Item1 == 0)
                    {
                        //We are on the leftmost side of the image so only do horizontal overlap logic
                        //imageB2[bX, bY] = Color.White;
                        //If our path is horizontal (we are tiling vertically)
                        if (y - beginY > shortestPathArrayH[x - beginX] || patchB.Item2 == 0)
                        {
                            //If we're below the path, put in color from imageA2
                            //Also if we're at the top of the image we want all of A2
                            imageB2[bX, bY] = imageA2[x, y];
                            //newColor = imageA2[x, y];
                        }
                        else
                        {
                            //If we're above or on the path, keep imageB2 as it is
                            //imageB2[bX, bY] = Color.Aqua;
                            //newColor = imageB2[bX, bY];
                        }
                    } else if(patchB.Item2 == 0)
                    {
                        //We are on the topmost side of the image so only do vertical overlap logic
                        //imageB2[bX, bY] = Color.Black;
                        //If our path is vertical (we are tiling horizontally)
                        if (x - beginX > shortestPathArrayV[y - beginY] || patchB.Item1 == 0)
                        {
                            //We're to the right of the path, put in color from imageA2
                            //Also if we're at the left of the image we want all of B2
                            imageB2[bX, bY] = imageA2[x, y];
                            //newColor = imageA2[x, y];
                        }
                        else
                        {
                            //We're to the left of the path, keep imageB2 as it is
                            //imageB2[bX, bY] = Color.Aqua;
                            //newColor = imageB2[bX, bY];
                        }
                    } else
                    {
                        //Every other case, inside the image
                        if (x - beginX < patchOverlap && y - beginY < patchOverlap)
                        {
                            //Top left corner where we need to blend
                            //imageB2[bX, bY] = Color.Red;
                            Color horizontalColor;
                            Color verticalColor;

                            bool leftActive = false;
                            bool topActive = false;
                            //If our path is vertical (we are tiling horizontally)
                            if (x - beginX > shortestPathArrayV[y - beginY])
                            {
                                //We're to the right of the path, put in color from imageA2
                                //Also if we're at the left of the image we want all of B2
                                //imageB2[bX, bY] = imageA2[x, y];
                                verticalColor = imageA2[x, y];
                            }
                            else
                            {
                                //We're to the left of the path, keep imageB2 as it is
                                //verticalColor = Color.Aqua;
                                leftActive = true;
                                verticalColor = imageB2[bX, bY];
                            }

                            //If our path is horizontal (we are tiling vertically)
                            if (y - beginY > shortestPathArrayH[x - beginX])
                            {
                                //If we're below the path, put in color from imageA2
                                //Also if we're at the top of the image we want all of A2
                                //imageB2[bX, bY] = imageA2[x, y];
                                horizontalColor = imageA2[x, y];
                            }
                            else
                            {
                                //If we're above or on the path, keep imageB2 as it is
                                //imageB2[bX, bY] = Color.Green;
                                topActive = true;
                                //horizontalColor = Color.Yellow;
                                horizontalColor = imageB2[bX, bY];
                            }

                            // imageB2[bX, bY] = Color.Red;
                            if (leftActive && topActive)
                            {
                                imageB2[bX, bY] = blendAverage(horizontalColor, verticalColor);
                            } else if (leftActive)
                            {
                                imageB2[bX, bY] = verticalColor;
                            } else if (topActive)
                            {
                                imageB2[bX, bY] = horizontalColor;
                            } else
                            {
                                imageB2[bX, bY] = blendAverage(horizontalColor, verticalColor);
                            }
                            

                        } else if (x-beginX < patchOverlap)
                        {
                            //Bottom left corner where we need vertical overlap logic
                            //imageB2[bX, bY] = Color.Blue;

                            //If our path is vertical (we are tiling horizontally)
                            if (x - beginX > shortestPathArrayV[y - beginY] || patchB.Item1 == 0)
                            {
                                //We're to the right of the path, put in color from imageA2
                                //Also if we're at the left of the image we want all of B2
                                imageB2[bX, bY] = imageA2[x, y];
                                //newColor = imageA2[x, y];
                            }
                            else
                            {
                                //We're to the left of the path, keep imageB2 as it is
                                //imageB2[bX, bY] = Color.Aqua;
                                //newColor = imageB2[bX, bY];
                            }
                        } else if (y-beginY < patchOverlap)
                        {
                            //Top right corner where we need horizontal overlap logic

                            //imageB2[bX, bY] = Color.Yellow;
                            //If our path is horizontal (we are tiling vertically)
                            if (y - beginY > shortestPathArrayH[x - beginX] || patchB.Item2 == 0)
                            {
                                //If we're below the path, put in color from imageA2
                                //Also if we're at the top of the image we want all of A2
                                imageB2[bX, bY] = imageA2[x, y];
                                //newColor = imageA2[x, y];
                            }
                            else
                            {
                                //If we're above or on the path, keep imageB2 as it is
                                //imageB2[bX, bY] = Color.Yellow;
                                //newColor = imageB2[bX, bY];
                            }
                        } else
                        {
                            //Bottom right corner where we need imageA2 values
                            //imageB2[bX, bY] = Color.Green;
                            imageB2[bX, bY] = imageA2[x, y];
                        }
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
            Tuple<int, int>[,] newPrevMatches = new Tuple<int, int>[width, height];
            Tuple<int, int> bestMatch = null;

            //TESTING TO MAKE SURE DIJKSTRA ACTUALLY WORKS
            int patchIterX = patchIter;
            int patchIterY = patchIter;

            /*Iterate through patches finding a best match for each */
            for (int col = 0; col < width; col += patchIterX)
            {
                //if (col > 10)
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

                    //Find the best random patch, not completely random since it takes existing patches into account
                    bestMatch = BestRandomPatchNew(imageB1, imageB2, col, row, patchNum);
                    newPrevMatches[col, row] = bestMatch;

                    //Copy the patch, first with horizontal overlap then with vertical overlap
                    Tuple<int,int> currentBPatch = new Tuple<int, int>(col, row);
                    imageB2 = copyPatchDijkstraSimultaneous(imageA2, imageB2, bestMatch, currentBPatch, true);

                    // Console.WriteLine("Current patch index: " + col + ", " + row)
                    Utilities.print("Current patch index: " + col + ", " + row);
                }
            }
            currentPrevMatches = newPrevMatches;
            return imageB2;
        }
    }
}
