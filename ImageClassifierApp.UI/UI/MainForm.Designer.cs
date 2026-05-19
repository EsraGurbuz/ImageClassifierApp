namespace ImageClassifierApp.UI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSelectImage = new Button();
            btnClassify = new Button();
            picSelectedImage = new PictureBox();
            lblResult = new Label();
            ((System.ComponentModel.ISupportInitialize)picSelectedImage).BeginInit();
            SuspendLayout();
            // 
            // btnSelectImage
            // 
            btnSelectImage.Location = new Point(553, 38);
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.Size = new Size(94, 29);
            btnSelectImage.TabIndex = 0;
            btnSelectImage.Text = "Select Image";
            btnSelectImage.UseVisualStyleBackColor = true;
            btnSelectImage.Click += this.button1_Click;
            // 
            // btnClassify
            // 
            btnClassify.Location = new Point(553, 73);
            btnClassify.Name = "btnClassify";
            btnClassify.Size = new Size(94, 29);
            btnClassify.TabIndex = 1;
            btnClassify.Text = "Classify";
            btnClassify.UseVisualStyleBackColor = true;
            btnClassify.Click += this.button1_Click_1;
            // 
            // picSelectedImage
            // 
            picSelectedImage.Location = new Point(431, 206);
            picSelectedImage.Name = "picSelectedImage";
            picSelectedImage.Size = new Size(125, 62);
            picSelectedImage.SizeMode = PictureBoxSizeMode.Zoom;
            picSelectedImage.TabIndex = 2;
            picSelectedImage.TabStop = false;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Location = new Point(518, 164);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(126, 20);
            lblResult.TabIndex = 3;
            lblResult.Text = "Result: Expected...";
            lblResult.Click += this.label1_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblResult);
            Controls.Add(picSelectedImage);
            Controls.Add(btnClassify);
            Controls.Add(btnSelectImage);
            Name = "MainForm";
            Text = "Form1";
            Load += this.MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)picSelectedImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSelectImage;
        private Button btnClassify;
        private PictureBox picSelectedImage;
        private Label lblResult;
    }
}
