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
            Color[,] imageA1 = Utilities.createImageArrayFromFile("TestImages/kpopA1.png");
            Color[,] imageA2 = Utilities.createImageArrayFromFile("TestImages/kpopA2.png");

            /* Image we want to generate a pair out of*/
            Color[,] imageB1 = Utilities.createImageArrayFromFile("TestImages/kpopB1.png");

            ImageAnalogy ia = new ImageAnalogy(imageA1, imageA2, 5, 2);

            Color[,] imageB2 = ia.CreateImageAnalogy(imageB1, 5000);
            Utilities.createFileFromImageArray(imageB2, "TestImages/testOutputKpop2.png");

            Console.ReadLine();
        }
    }
}
