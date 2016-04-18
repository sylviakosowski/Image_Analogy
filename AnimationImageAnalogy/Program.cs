using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace AnimationImageAnalogy
{
    class Program
    {

        [STAThread]
        static void Main()
        {

            if(Utilities.CONSOLE)
            {
                //Run this in console mode
                string pathA1 = "C:/Users/sylvi_000/Documents/College/BXA_Capstone/Images/Character/ImagesA1";
                string pathA2 = "C:/Users/sylvi_000/Documents/College/BXA_Capstone/Images/Character/ImagesA2";
                string pathB1 = "C:/Users/sylvi_000/Documents/College/BXA_Capstone/Images/Character/ImagesB1";
                string pathB2 = "C:/Users/sylvi_000/Documents/College/BXA_Capstone/Images/Character/ImagesB2/coherence_test_shift_radius10";
                int patchSize = 10;
                int patchIter = 6;
                int patchRand = 2000;
                int coherenceRadius = 10;


                new CreateFrames(pathA1, pathA2, pathB1, pathB2, patchSize, patchIter, patchRand, coherenceRadius);
            }
            else
            {
                //Run this in application mode
                Application.Run(new PainterlyAnimationTool());
            }
            

            

            /* The source image pair */
            /*
            //Color[,] imageA1 = Utilities.createImageArrayFromFile("TestImages/imageA1.png");
            //Color[,] imageA2 = Utilities.createImageArrayFromFile("TestImages/imageA2.png");
            Color[,] imageA1 = Utilities.createImageArrayFromFile("TestImages/characterTest/characterA1.png");
            Color[,] imageA2 = Utilities.createImageArrayFromFile("TestImages/characterTest/characterA2.png");

            //Image we want to generate a pair out of
            //Color[,] imageB1 = Utilities.createImageArrayFromFile("TestImages/imageB1.png");
            Color[,] imageB1 = Utilities.createImageArrayFromFile("TestImages/characterTest/characterB1.png");

            ImageAnalogy ia = new ImageAnalogy(imageA1, imageA2, 10, 6);

            Color[,] imageB2 = ia.CreateImageAnalogy(imageB1, 2000);
            //Utilities.createFileFromImageArray(imageB2, "TestImages/riverOutput/test_new_best_patch_20_12_2000.png");
            Utilities.createFileFromImageArray(imageB2, "TestImages/characterTest/new_best_patch_10_6_2000_TEST_AFTER_CLEANUP.png");

            Console.ReadLine();
            */
        }
    }
}
