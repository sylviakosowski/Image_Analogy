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
            Color[,] imageA1 = Utilities.createImageArrayFromFile("TestImages/monetA1.png");
            Color[,] imageA2 = Utilities.createImageArrayFromFile("TestImages/monetA2.png");

            /* Image we want to generate a pair out of*/
            Color[,] imageB1 = Utilities.createImageArrayFromFile("TestImages/ballbounce6.png");

            ImageAnalogy ia = new ImageAnalogy(imageA1, imageA2, 20, 10);

            Color[,] imageB2 = ia.CreateImageAnalogy(imageB1, 1000);
            Utilities.createFileFromImageArray(imageB2, "TestImages/ballBounce6-monet.png");

            Console.ReadLine();
        }
    }
}
