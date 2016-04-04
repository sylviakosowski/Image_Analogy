using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }
    }
}
