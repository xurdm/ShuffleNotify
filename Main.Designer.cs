namespace ShuffleNotify
{
    partial class Main
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
            btnStartTracking = new Button();
            lblStatus = new Label();
            pbScreenshot = new PictureBox();
            btnStopTracking = new Button();
            lblText = new Label();
            ((System.ComponentModel.ISupportInitialize)pbScreenshot).BeginInit();
            SuspendLayout();
            // 
            // btnStartTracking
            // 
            btnStartTracking.Location = new Point(12, 51);
            btnStartTracking.Name = "btnStartTracking";
            btnStartTracking.Size = new Size(75, 23);
            btnStartTracking.TabIndex = 0;
            btnStartTracking.Text = "Start";
            btnStartTracking.UseVisualStyleBackColor = true;
            btnStartTracking.Click += btnStartTracking_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(26, 23);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(38, 15);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "label1";
            // 
            // pbScreenshot
            // 
            pbScreenshot.Location = new Point(194, 96);
            pbScreenshot.Name = "pbScreenshot";
            pbScreenshot.Size = new Size(600, 405);
            pbScreenshot.SizeMode = PictureBoxSizeMode.StretchImage;
            pbScreenshot.TabIndex = 2;
            pbScreenshot.TabStop = false;
            // 
            // btnStopTracking
            // 
            btnStopTracking.Location = new Point(93, 51);
            btnStopTracking.Name = "btnStopTracking";
            btnStopTracking.Size = new Size(75, 23);
            btnStopTracking.TabIndex = 3;
            btnStopTracking.Text = "Stop";
            btnStopTracking.UseVisualStyleBackColor = true;
            btnStopTracking.Click += btnStopTracking_Click;
            // 
            // lblText
            // 
            lblText.Location = new Point(12, 96);
            lblText.Name = "lblText";
            lblText.Size = new Size(176, 405);
            lblText.TabIndex = 4;
            lblText.Text = "label1";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(806, 510);
            Controls.Add(lblText);
            Controls.Add(btnStopTracking);
            Controls.Add(pbScreenshot);
            Controls.Add(lblStatus);
            Controls.Add(btnStartTracking);
            Name = "Main";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pbScreenshot).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStartTracking;
        private Label lblStatus;
        private PictureBox pbScreenshot;
        private Button btnStopTracking;
        private Label lblText;
    }
}
