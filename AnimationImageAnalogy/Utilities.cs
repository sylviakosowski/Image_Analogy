using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace AnimationImageAnalogy
{
    /* Provides various image related functions. */
    public static class Utilities
    {
        /* Creates a two dimensional array of colors representing the color
         * of each pixel in an image. The source image is given as a string. */
        public static Color[,] createImageArrayFromFile(string file)
        {
            Color[,] image;
            using(Bitmap bmp = new Bitmap(file)) {
                
                image = new Color[bmp.Width, bmp.Height];

                for (int i = 0; i < bmp.Width; i++)
                {
                    for (int j = 0; j < bmp.Height; j++)
                    {
                        image[i, j] = bmp.GetPixel(i, j);
                    }
                }
            } 

            return image;         
        }

        /*Given an array of colors, create an image file*/
        public static void createFileFromImageArray(Color[,] image)
        {
            using (Bitmap bmp = new Bitmap(image.GetLength(0), image.GetLength(1)))
            {
                for (int i = 0; i < bmp.Width; i++)
                {
                    for(int j = 0; j < bmp.Height; j++)
                    {
                        bmp.SetPixel(i,j, image[i,j]);
                    }
                }

                Console.WriteLine("Saving image");
                bmp.Save("TestImages/testOutput.png", ImageFormat.Png);
            }


        }

    }
}
