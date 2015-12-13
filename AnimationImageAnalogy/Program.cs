using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AnimationImageAnalogy
{
    class Program
    {
        static void Main(string[] args)
        {
            /* The source image pair */
            Color[,] imageA1 = Utilities.createImageArrayFromFile("TestImages/imageA1-small.png");
            Color[,] imageA2 = Utilities.createImageArrayFromFile("TestImages/imageA2-small.png");

            /* Image we want to generate a pair out of*/
            Color[,] imageB1 = Utilities.createImageArrayFromFile("TestImages/imageB1-small.png");

            ImageAnalogy ia = new ImageAnalogy(imageA1, imageA2, 5, 3);

            Color[,] imageB2 = ia.CreateImageAnalogy(imageB1);
            Utilities.createFileFromImageArray(imageB2, "TestImages/testOutputRiver-small.png");

            Console.ReadLine();
        }
    }
}
