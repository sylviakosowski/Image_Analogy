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
            Color[,] imageA1 = Utilities.createImageArrayFromFile("TestImages/characterTest/characterA1.png");
            Color[,] imageA2 = Utilities.createImageArrayFromFile("TestImages/characterTest/characterA2.png");

            /* Image we want to generate a pair out of*/
            Color[,] imageB1 = Utilities.createImageArrayFromFile("TestImages/characterTest/characterB1.png");

            ImageAnalogy ia = new ImageAnalogy(imageA1, imageA2, 20, 12);

            Color[,] imageB2 = ia.CreateImageAnalogy(imageB1, 2000);
            Utilities.createFileFromImageArray(imageB2, "TestImages/characterTest/test_simult_20_12_2000.png");
            //Utilities.createFileFromImageArray(imageB2, "TestImages/riverOutput/Testing/test_simultaneous_new_20_12.png");

            Console.ReadLine();
        }
    }
}
