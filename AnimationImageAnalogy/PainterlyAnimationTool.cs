using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimationImageAnalogy
{
    public partial class PainterlyAnimationTool : Form
    {
        public PainterlyAnimationTool()
        {
            InitializeComponent();
        }

        private void PainterlyAnimationTool_Load(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void pathA1Browse_Click(object sender, EventArgs e)
        {
            //Choose a folder and display in path dialog box.
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pathA1Text.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void pathA2Browse_Click(object sender, EventArgs e)
        {
            //Choose a folder and display in path dialog box.
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pathA2Text.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void pathB1Browse_Click(object sender, EventArgs e)
        {
            //Choose a folder and display in path dialog box.
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pathB1Text.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void pathB2Browse_Click(object sender, EventArgs e)
        {
            //Choose a folder and display in path dialog box.
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pathB2Text.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
