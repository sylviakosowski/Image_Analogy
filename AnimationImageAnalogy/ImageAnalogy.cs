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
        }

        /* Compute patch value
         * patchLocA is the top left corner of the patch in A
         * patchLocB is the top left corner of the patch in B
         */
        private int ComputePatchValue(Color[,] imageA, Color[,] imageB, 
            Tuple <int,int> patchLocA, Tuple<int,int> patchLocB)
        {
            int patchValue = 0;
            for (int i = 0; i < patchDimension; i++) 
            {
                for (int j = 0; j < patchDimension; j++)
                {
                    //Compute sum of squared differences for each pixel in patch
                    Color a = imageA[patchLocA.Item1+i, patchLocA.Item2+j];
                    Color b = imageB[patchLocB.Item1+i, patchLocB.Item2+j];

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
        private Tuple<int,int> BestPatchMatch(Color[,] imageB1, Tuple<int,int> patchB1)
        {
            Tuple<int, int> bestPatchCoord = null;



            return bestPatchCoord;
        }

        /* 
         * Create an image analogy for the given image using the source pair.
         */
        public Color[,] CreateImageAnalogy(Color[,] imageB1)
        {
            int width = imageB1.GetLength(0);
            int height = imageB1.GetLength(1);
            Color[,] imageB2 = new Color[width, height];

            /*Iterate through patches finding a best match for each */
            for (int i = 0; i < width; i += patchDimension){
                for (int j = 0; j < height; j += patchDimension) {
                    
                    
                }
            }

            return imageB2;
        }



    }
}
