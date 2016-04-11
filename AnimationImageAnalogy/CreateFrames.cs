using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace AnimationImageAnalogy
{
    class CreateFrames
    {
        private PainterlyAnimationTool ui;

        /* Variables used to store data from the form. */
        private string pathA1;
        private string pathA2;
        private string pathB1;
        private string pathB2;
        private int patchSize;
        private int patchIter;
        private int randAmount;

        private string[] framesA1;
        private string[] framesA2;
        private string[] framesB1;

        /* Constructor, takes in the UI form. */
        public CreateFrames(PainterlyAnimationTool ui, string pathA1, string pathA2, string pathB1, string pathB2,
            int patchSize, int patchIter, int randAmount)
        {
            this.ui = ui;
            this.pathA1 = pathA1;
            this.pathA2 = pathA2;
            this.pathB1 = pathB1;
            this.pathB2 = pathB2;
            this.patchSize = patchSize;
            this.patchIter = patchIter;
            this.randAmount = randAmount;
            iterFiles();
        }

        /* Iterate through each frame, generating the animation frame and copying over
         * all the frames (including keyframes) into the imageB2 path folder.
         */
        private void iterFiles()
        {
            //Get all the image files from the provided directories
            framesA1 = Directory.GetFiles(pathA1);
            framesA2 = Directory.GetFiles(pathA2);
            framesB1 = Directory.GetFiles(pathB1);
            Array.Sort(framesA1);
            Array.Sort(framesA2);
            Array.Sort(framesB1);

            //Set keyframe values
            int startKeyIndex = 0;
            int endKeyIndex = startKeyIndex + 1;
            int startKeyNum = parseFrameFromName(framesA1[startKeyIndex]);
            int endKeyNum = parseFrameFromName(framesA1[endKeyIndex]);
            ImageAnalogy startAnalogy = createAnalogy(framesA1[startKeyIndex], framesA2[startKeyIndex]);
            ImageAnalogy endAnalogy = createAnalogy(framesA1[endKeyIndex], framesA2[endKeyIndex]);

            //Copy the current keyframes to destination directory
            copyFrame(framesA2[startKeyIndex]);
            copyFrame(framesA2[endKeyIndex]);

            //Iterate through the in-between frames, creating a new frame for each
            foreach(string frame in framesB1)
            {
                //Adjust current start and end key frames if the in-between we're on
                //is not between them.
                int currentFrameNum = parseFrameFromName(frame);
                if(currentFrameNum > endKeyNum)
                {
                    startKeyIndex++;
                    endKeyIndex++;

                    //We've reached the end
                    if (endKeyIndex >= framesA1.Count())
                    {
                        return;
                    }

                    //Change values for keyframes
                    startKeyNum = parseFrameFromName(framesA1[startKeyIndex]);
                    endKeyNum = parseFrameFromName(framesA1[endKeyIndex]);
                    startAnalogy = endAnalogy;
                    endAnalogy = createAnalogy(framesA1[endKeyIndex], framesA2[endKeyIndex]);

                    //Make sure keyframes get copied over as the index increases
                    copyFrame(framesA2[endKeyIndex]);
                }

                ui.outputBox.Text += "CREATING IN-BETWEEN FRAME: " + Path.GetFileName(frame) + Environment.NewLine;

                //FOR TESTING PURPOSES WE'RE JUST DOING ONE TO SEE IF WE GET THE SAME RESULTS USING THIS NEW SYSTEM
                Color[,] imageFromStart = createImage(startAnalogy, frame);
                //Color[,] imageFromEnd = createImage(endAnalogy, frame);

                //FOR NOW, BLEND 50-50 BETWEEN START AND END. IN THE FUTURE, WE'LL WEIGHT BASED
                //ON HOW CLSE THE IN BETWEEN IS TO THE START AND END KEYFRAMES
                //Color[,] average = Utilities.averageArrays(imageFromStart, imageFromEnd, 0.50f);
                //writeImage(average, frame);
                writeImage(imageFromStart, frame);
            }

        }

        /* Helper function to parse out the frame number from the given file's name */
        private int parseFrameFromName(string name)
        {
            string fileName = Path.GetFileName(name);
            var nameSplit = fileName.Split('_');
            string numbers = nameSplit[1];
            return Int32.Parse(numbers);
        }

        /* Copy the provided frame to the destination directory */
        private void copyFrame(string frameA2)
        {
            string frameName = Path.GetFileName(frameA2);
            ui.outputBox.Text += "COPYING KEYFRAME: " + frameName + Environment.NewLine;
            File.Copy(frameA2, Path.Combine(pathB2, frameName));
        }

        /* Create an image analogy between the provided images with provided parameters. */
        private ImageAnalogy createAnalogy(string frameA1, string frameA2)
        {
            //Image source pair data
            Color[,] imageA1 = Utilities.createImageArrayFromFile(frameA1);
            Color[,] imageA2 = Utilities.createImageArrayFromFile(frameA2);
            return new ImageAnalogy(imageA1, imageA2, patchSize, patchIter, ui);
        }

        /* Creates an animation frame using the provided analogy, image, and parameters */
        private Color[,] createImage(ImageAnalogy ia, string frameB1)
        {
            Color[,] imageB1 = Utilities.createImageArrayFromFile(frameB1);
            Color[,] imageB2 = ia.CreateImageAnalogy(imageB1, randAmount);
            return imageB2;
        }

        /* Copy the final B2 image to the B2 output path */
        private void writeImage(Color[,] imageB2, string frameName)
        {
            ui.outputBox.Text += "SAVING IN-BETWEEN FRAME: " + Path.GetFileName(frameName) + Environment.NewLine;

            string newFilePath = Path.Combine(pathB2, Path.GetFileName(frameName));
            Utilities.createFileFromImageArray(imageB2, newFilePath);
        }
    }
}
