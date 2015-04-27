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
        private int patchDimension;

        /* Constructor */
        public ImageAnalogy(Color[,] imageA1, Color[,] imageA2, int patchDimension) 
        {
            this.imageA1 = imageA1;
            this.imageA2 = imageA2;
            this.patchDimension = patchDimension;
        }

        /* Compute patch value
         * patchLocA is the top left corner of the patch in A
         * patchLocB is the top left corner of the patch in B
         */
        private int ComputePatchValue(Color[,] imageA, Color[,] imageB, 
            Tuple <int,int> patchLocA, int bX, int bY)
        {
            int patchValue = 0;
            for (int i = 0; i < patchDimension; i++) 
            {
                for (int j = 0; j < patchDimension; j++)
                {
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
            for (int i = 0; i < width; i += patchDimension)
            {
                for (int j = 0; j < height; j += patchDimension)
                {
                    Tuple<int,int> currentPatchA1 = new Tuple<int,int>(i,j);
                    currentPatchVal = ComputePatchValue(imageA1, imageB1, currentPatchA1, bX, bY);
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
         * the patch with top left corner at indices bX, bY in imageB2.
         */
        private Color[,] copyPatch(Color[,] imageA2, Color[,] imageB2, Tuple<int,int> patchA, int bX, int bY)
        {
            for(int i = patchA.Item1; i < i+patchDimension; i++)
            {
                for (int j = patchA.Item2; j < j+patchDimension; j++)
                {
                    imageB2[bX, bY] = imageA2[i, j];
                    bY++;
                }
                bX++;
            }
            return imageB2;
        }

        /* 
         * Create an image analogy for the given image using the source pair.
         */
        public Color[,] CreateImageAnalogy(Color[,] imageB1)
        {
            int width = imageB1.GetLength(0);
            int height = imageB1.GetLength(1);
            Color[,] imageB2 = new Color[width, height];

            Tuple<int, int> bestMatch = null;
            /*Iterate through patches finding a best match for each */
            for (int i = 0; i < width; i += patchDimension){
                for (int j = 0; j < height; j += patchDimension) {
                    bestMatch = BestPatchMatch(imageB1, i, j);
                    imageB2 = copyPatch(imageA2, imageB2, bestMatch, i, j);
                    Console.WriteLine("Current patch index: " + i + ", " + j);
                }
                if (i > 20)
                {
                    break;
                }
            }

            return imageB2;
        }



    }
}
