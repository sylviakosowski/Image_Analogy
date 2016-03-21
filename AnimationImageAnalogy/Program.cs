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
            Color[,] imageA1 = Utilities.createImageArrayFromFile("TestImages/imageA1.png");
            Color[,] imageA2 = Utilities.createImageArrayFromFile("TestImages/imageA2.png");
            //Color[,] imageA1 = Utilities.createImageArrayFromFile("TestImages/characterTest/characterA1.png");
            //Color[,] imageA2 = Utilities.createImageArrayFromFile("TestImages/characterTest/characterA2.png");

            /* Image we want to generate a pair out of*/
            Color[,] imageB1 = Utilities.createImageArrayFromFile("TestImages/imageB1.png");
            //Color[,] imageB1 = Utilities.createImageArrayFromFile("TestImages/characterTest/characterB1.png");

            ImageAnalogy ia = new ImageAnalogy(imageA1, imageA2, 20, 12);

            Color[,] imageB2 = ia.CreateImageAnalogy(imageB1, 2000);
            Utilities.createFileFromImageArray(imageB2, "TestImages/riverOutput/test_new_best_patch.png");
            //Utilities.createFileFromImageArray(imageB2, "TestImages/riverOutput/newDijkstra/COHERENCE_TEST_20_12_2000_test_10_60.png");

            Console.ReadLine();
        }
    }
}
