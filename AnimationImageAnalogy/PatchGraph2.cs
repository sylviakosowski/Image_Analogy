using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AnimationImageAnalogy
{
    class PatchGraph2
    {
        private int patchDimension; //the dimension of the patch
        private int patchIter; //number of pixels we iterate by each time

        public PatchGraph2(int patchDimension, int patchIter)
        {
            this.patchDimension = patchDimension;
            this.patchIter = patchIter;
        }

        /* Construct a graph out of pixels where the value at each node is the difference 
         * between the pixel components of the existing patch from B2 and the new patch from
         * A2. Edges are weighted with the sum of squared differences between adjacent pixels. */

    }
}
