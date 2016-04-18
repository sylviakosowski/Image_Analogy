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
        public static bool CONSOLE = true;

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
        public static void createFileFromImageArray(Color[,] image, string name)
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

                //Console.WriteLine("Saving image");
                Logger.Log("Saving image");
                bmp.Save(name, ImageFormat.Png);
            }


        }

        /* Average two arrays. Precondition: they have the same dimensions. */
        public static Color[,] averageArrays(Color[,] image1, Color[,] image2, float weight)
        {
            int height = image1.GetLength(0);
            int width = image1.GetLength(1);
            Color[,] average = new Color[height, width];
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    average[i, j] = blendWeightedAverage(image1[i, j], image2[i, j], weight);
                }
            }

            return average;
        }

        private static Color blendWeightedAverage(Color a, Color b, float weight)
        {
            //Color current = imageB2[bX, bY];
            //Color aColor = imageA2[aX, aY];
            ///int aVal = (a.A + b.A) / 2;
            //int rVal = (a.R + b.R) / 2;
            //int gVal = (a.G + b.G) / 2;
            //int bVal = (a.B + b.B) / 2;
            int aVal = (int)((a.A * weight) + (b.A * (1 - weight)));
            int rVal = (int)((a.R * weight) + (b.R * (1 - weight)));
            int gVal = (int)((a.G * weight) + (b.G * (1 - weight)));
            int bVal = (int)((a.B * weight) + (b.B * (1 - weight)));
            Color average = Color.FromArgb(aVal, rVal, gVal, bVal);
            return average;
        }

        public static void print(string str)
        {
            if (CONSOLE)
            {
                Console.WriteLine(str);
            }
            else
            {
                Logger.Log(str);
            }
        }

    }
}
