namespace AnimationImageAnalogy
{
    partial class PainterlyAnimationTool
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pathA1Browse = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pathA1Text = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pathA2Browse = new System.Windows.Forms.Button();
            this.pathA2Text = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pathB1Browse = new System.Windows.Forms.Button();
            this.pathB1Text = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pathB2Browse = new System.Windows.Forms.Button();
            this.pathB2Text = new System.Windows.Forms.TextBox();
            this.createFramesButton = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.optionsGroup = new System.Windows.Forms.GroupBox();
            this.randText = new System.Windows.Forms.TextBox();
            this.randLabel = new System.Windows.Forms.Label();
            this.iterText = new System.Windows.Forms.TextBox();
            this.patchIterLabel = new System.Windows.Forms.Label();
            this.sizeText = new System.Windows.Forms.TextBox();
            this.patchSizeLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.optionsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // pathA1Browse
            // 
            this.pathA1Browse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.pathA1Browse.Location = new System.Drawing.Point(742, 52);
            this.pathA1Browse.Margin = new System.Windows.Forms.Padding(2);
            this.pathA1Browse.Name = "pathA1Browse";
            this.pathA1Browse.Size = new System.Drawing.Size(138, 42);
            this.pathA1Browse.TabIndex = 0;
            this.pathA1Browse.Text = "Browse";
            this.pathA1Browse.UseVisualStyleBackColor = true;
            this.pathA1Browse.Click += new System.EventHandler(this.pathA1Browse_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pathA1Text);
            this.groupBox1.Controls.Add(this.pathA1Browse);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.groupBox1.Location = new System.Drawing.Point(17, 23);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(897, 113);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source Images A1";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // pathA1Text
            // 
            this.pathA1Text.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.pathA1Text.Location = new System.Drawing.Point(19, 52);
            this.pathA1Text.Margin = new System.Windows.Forms.Padding(2);
            this.pathA1Text.Name = "pathA1Text";
            this.pathA1Text.Size = new System.Drawing.Size(710, 68);
            this.pathA1Text.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pathA2Browse);
            this.groupBox2.Controls.Add(this.pathA2Text);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.groupBox2.Location = new System.Drawing.Point(17, 148);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(897, 100);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Source Images A2";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // pathA2Browse
            // 
            this.pathA2Browse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.pathA2Browse.Location = new System.Drawing.Point(742, 46);
            this.pathA2Browse.Margin = new System.Windows.Forms.Padding(2);
            this.pathA2Browse.Name = "pathA2Browse";
            this.pathA2Browse.Size = new System.Drawing.Size(138, 40);
            this.pathA2Browse.TabIndex = 2;
            this.pathA2Browse.Text = "Browse";
            this.pathA2Browse.UseVisualStyleBackColor = true;
            this.pathA2Browse.Click += new System.EventHandler(this.pathA2Browse_Click);
            // 
            // pathA2Text
            // 
            this.pathA2Text.Location = new System.Drawing.Point(19, 42);
            this.pathA2Text.Margin = new System.Windows.Forms.Padding(2);
            this.pathA2Text.Name = "pathA2Text";
            this.pathA2Text.Size = new System.Drawing.Size(710, 68);
            this.pathA2Text.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pathB1Browse);
            this.groupBox3.Controls.Add(this.pathB1Text);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.groupBox3.Location = new System.Drawing.Point(17, 252);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(897, 95);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Source Images B1";
            // 
            // pathB1Browse
            // 
            this.pathB1Browse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.pathB1Browse.Location = new System.Drawing.Point(742, 38);
            this.pathB1Browse.Margin = new System.Windows.Forms.Padding(2);
            this.pathB1Browse.Name = "pathB1Browse";
            this.pathB1Browse.Size = new System.Drawing.Size(138, 42);
            this.pathB1Browse.TabIndex = 2;
            this.pathB1Browse.Text = "Browse";
            this.pathB1Browse.UseVisualStyleBackColor = true;
            this.pathB1Browse.Click += new System.EventHandler(this.pathB1Browse_Click);
            // 
            // pathB1Text
            // 
            this.pathB1Text.Location = new System.Drawing.Point(19, 36);
            this.pathB1Text.Margin = new System.Windows.Forms.Padding(2);
            this.pathB1Text.Name = "pathB1Text";
            this.pathB1Text.Size = new System.Drawing.Size(710, 68);
            this.pathB1Text.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pathB2Browse);
            this.groupBox4.Controls.Add(this.pathB2Text);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.groupBox4.Location = new System.Drawing.Point(17, 351);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(897, 102);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Destination B2";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // pathB2Browse
            // 
            this.pathB2Browse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.pathB2Browse.Location = new System.Drawing.Point(742, 43);
            this.pathB2Browse.Margin = new System.Windows.Forms.Padding(2);
            this.pathB2Browse.Name = "pathB2Browse";
            this.pathB2Browse.Size = new System.Drawing.Size(138, 37);
            this.pathB2Browse.TabIndex = 2;
            this.pathB2Browse.Text = "Browse";
            this.pathB2Browse.UseVisualStyleBackColor = true;
            this.pathB2Browse.Click += new System.EventHandler(this.pathB2Browse_Click);
            // 
            // pathB2Text
            // 
            this.pathB2Text.Location = new System.Drawing.Point(19, 40);
            this.pathB2Text.Margin = new System.Windows.Forms.Padding(2);
            this.pathB2Text.Name = "pathB2Text";
            this.pathB2Text.Size = new System.Drawing.Size(710, 68);
            this.pathB2Text.TabIndex = 1;
            // 
            // createFramesButton
            // 
            this.createFramesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.createFramesButton.Location = new System.Drawing.Point(17, 1136);
            this.createFramesButton.Margin = new System.Windows.Forms.Padding(2);
            this.createFramesButton.Name = "createFramesButton";
            this.createFramesButton.Size = new System.Drawing.Size(897, 104);
            this.createFramesButton.TabIndex = 6;
            this.createFramesButton.Text = "Create Frames";
            this.createFramesButton.UseVisualStyleBackColor = true;
            this.createFramesButton.Click += new System.EventHandler(this.createFramesButton_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.progressBar);
            this.groupBox5.Controls.Add(this.outputBox);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.groupBox5.Location = new System.Drawing.Point(17, 586);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(897, 536);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Output";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(14, 485);
            this.progressBar.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(866, 36);
            this.progressBar.TabIndex = 1;
            // 
            // outputBox
            // 
            this.outputBox.Location = new System.Drawing.Point(14, 38);
            this.outputBox.Margin = new System.Windows.Forms.Padding(2);
            this.outputBox.Multiline = true;
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(868, 425);
            this.outputBox.TabIndex = 0;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // optionsGroup
            // 
            this.optionsGroup.Controls.Add(this.randText);
            this.optionsGroup.Controls.Add(this.randLabel);
            this.optionsGroup.Controls.Add(this.iterText);
            this.optionsGroup.Controls.Add(this.patchIterLabel);
            this.optionsGroup.Controls.Add(this.sizeText);
            this.optionsGroup.Controls.Add(this.patchSizeLabel);
            this.optionsGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.optionsGroup.Location = new System.Drawing.Point(17, 472);
            this.optionsGroup.Margin = new System.Windows.Forms.Padding(2);
            this.optionsGroup.Name = "optionsGroup";
            this.optionsGroup.Padding = new System.Windows.Forms.Padding(2);
            this.optionsGroup.Size = new System.Drawing.Size(897, 110);
            this.optionsGroup.TabIndex = 8;
            this.optionsGroup.TabStop = false;
            this.optionsGroup.Text = "Options";
            // 
            // randText
            // 
            this.randText.Location = new System.Drawing.Point(758, 51);
            this.randText.Margin = new System.Windows.Forms.Padding(2);
            this.randText.Name = "randText";
            this.randText.Size = new System.Drawing.Size(124, 68);
            this.randText.TabIndex = 6;
            // 
            // randLabel
            // 
            this.randLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.randLabel.Location = new System.Drawing.Point(566, 55);
            this.randLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.randLabel.Name = "randLabel";
            this.randLabel.Size = new System.Drawing.Size(208, 53);
            this.randLabel.TabIndex = 5;
            this.randLabel.Text = "Rand Amount:";
            // 
            // iterText
            // 
            this.iterText.Location = new System.Drawing.Point(438, 51);
            this.iterText.Margin = new System.Windows.Forms.Padding(2);
            this.iterText.Name = "iterText";
            this.iterText.Size = new System.Drawing.Size(124, 68);
            this.iterText.TabIndex = 4;
            // 
            // patchIterLabel
            // 
            this.patchIterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.patchIterLabel.Location = new System.Drawing.Point(296, 55);
            this.patchIterLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.patchIterLabel.Name = "patchIterLabel";
            this.patchIterLabel.Size = new System.Drawing.Size(177, 53);
            this.patchIterLabel.TabIndex = 3;
            this.patchIterLabel.Text = "Patch Iter:";
            // 
            // sizeText
            // 
            this.sizeText.Location = new System.Drawing.Point(166, 51);
            this.sizeText.Margin = new System.Windows.Forms.Padding(2);
            this.sizeText.Name = "sizeText";
            this.sizeText.Size = new System.Drawing.Size(124, 68);
            this.sizeText.TabIndex = 2;
            // 
            // patchSizeLabel
            // 
            this.patchSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.patchSizeLabel.Location = new System.Drawing.Point(13, 55);
            this.patchSizeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.patchSizeLabel.Name = "patchSizeLabel";
            this.patchSizeLabel.Size = new System.Drawing.Size(177, 53);
            this.patchSizeLabel.TabIndex = 0;
            this.patchSizeLabel.Text = "Patch Size:";
            // 
            // PainterlyAnimationTool
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(938, 1266);
            this.Controls.Add(this.optionsGroup);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.createFramesButton);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.Location = new System.Drawing.Point(100, 100);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PainterlyAnimationTool";
            this.Padding = new System.Windows.Forms.Padding(12, 13, 12, 13);
            this.Text = "PainterlyAnimationTool";
            this.Load += new System.EventHandler(this.PainterlyAnimationTool_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.optionsGroup.ResumeLayout(false);
            this.optionsGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button pathA1Browse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button pathA2Browse;
        private System.Windows.Forms.TextBox pathA2Text;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button pathB1Browse;
        private System.Windows.Forms.TextBox pathB1Text;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button pathB2Browse;
        private System.Windows.Forms.TextBox pathB2Text;
        private System.Windows.Forms.Button createFramesButton;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox pathA1Text;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox optionsGroup;
        private System.Windows.Forms.Label patchSizeLabel;
        private System.Windows.Forms.TextBox sizeText;
        private System.Windows.Forms.Label patchIterLabel;
        private System.Windows.Forms.TextBox iterText;
        private System.Windows.Forms.TextBox randText;
        private System.Windows.Forms.Label randLabel;
        public System.Windows.Forms.TextBox outputBox;
    }
}