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
