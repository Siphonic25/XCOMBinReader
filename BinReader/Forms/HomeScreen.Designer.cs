namespace BinReader
{
    partial class HomeScreen
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
            ofdBin = new OpenFileDialog();
            buttonBin = new Button();
            SuspendLayout();
            // 
            // ofdBin
            // 
            ofdBin.FileName = "Select a bin file";
            ofdBin.Filter = "XCOM Bin files (*.bin)|*.bin";
            ofdBin.Title = "Open bin file";
            // 
            // buttonBin
            // 
            buttonBin.Location = new Point(12, 12);
            buttonBin.Name = "buttonBin";
            buttonBin.Size = new Size(139, 29);
            buttonBin.TabIndex = 0;
            buttonBin.Text = "Select A Bin File";
            buttonBin.UseVisualStyleBackColor = true;
            buttonBin.Click += buttonBin_Click;
            // 
            // HomeScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(942, 493);
            Controls.Add(buttonBin);
            Name = "HomeScreen";
            Text = "Bin Reader";
            ResumeLayout(false);
        }

        #endregion

        private OpenFileDialog ofdBin;
        private Button buttonBin;
    }
}
