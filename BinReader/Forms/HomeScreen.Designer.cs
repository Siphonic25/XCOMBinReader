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
            dgvPool = new DataGridView();
            firstName = new DataGridViewTextBoxColumn();
            lastName = new DataGridViewTextBoxColumn();
            nickName = new DataGridViewTextBoxColumn();
            soldierClass = new DataGridViewTextBoxColumn();
            gender = new DataGridViewTextBoxColumn();
            nationality = new DataGridViewTextBoxColumn();
            buttonSave = new Button();
            sfdbin = new SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)dgvPool).BeginInit();
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
            // dgvPool
            // 
            dgvPool.AllowUserToAddRows = false;
            dgvPool.AllowUserToDeleteRows = false;
            dgvPool.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPool.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPool.Columns.AddRange(new DataGridViewColumn[] { firstName, lastName, nickName, soldierClass, gender, nationality });
            dgvPool.Location = new Point(12, 61);
            dgvPool.Name = "dgvPool";
            dgvPool.ReadOnly = true;
            dgvPool.RowHeadersWidth = 51;
            dgvPool.Size = new Size(918, 377);
            dgvPool.TabIndex = 1;
            dgvPool.Visible = false;
            // 
            // firstName
            // 
            firstName.HeaderText = "First Name";
            firstName.MinimumWidth = 6;
            firstName.Name = "firstName";
            firstName.ReadOnly = true;
            firstName.Width = 109;
            // 
            // lastName
            // 
            lastName.HeaderText = "Last Name";
            lastName.MinimumWidth = 6;
            lastName.Name = "lastName";
            lastName.ReadOnly = true;
            lastName.Width = 108;
            // 
            // nickName
            // 
            nickName.HeaderText = "Callsign";
            nickName.MinimumWidth = 6;
            nickName.Name = "nickName";
            nickName.ReadOnly = true;
            nickName.Width = 90;
            // 
            // soldierClass
            // 
            soldierClass.HeaderText = "Class";
            soldierClass.MinimumWidth = 6;
            soldierClass.Name = "soldierClass";
            soldierClass.ReadOnly = true;
            soldierClass.Width = 71;
            // 
            // gender
            // 
            gender.HeaderText = "Gender";
            gender.MinimumWidth = 6;
            gender.Name = "gender";
            gender.ReadOnly = true;
            gender.Width = 86;
            // 
            // nationality
            // 
            nationality.HeaderText = "Nationality";
            nationality.MinimumWidth = 6;
            nationality.Name = "nationality";
            nationality.ReadOnly = true;
            nationality.Width = 111;
            // 
            // buttonSave
            // 
            buttonSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonSave.Location = new Point(12, 452);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(105, 29);
            buttonSave.TabIndex = 2;
            buttonSave.Text = "Save to Disk";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Visible = false;
            buttonSave.Click += buttonSave_Click;
            // 
            // sfdbin
            // 
            sfdbin.DefaultExt = "csv";
            sfdbin.FileName = "Pool.csv";
            sfdbin.Filter = "Spreadsheet (*.csv)|*.csv";
            // 
            // HomeScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(942, 493);
            Controls.Add(buttonSave);
            Controls.Add(dgvPool);
            Controls.Add(buttonBin);
            Name = "HomeScreen";
            Text = "Bin Reader";
            ((System.ComponentModel.ISupportInitialize)dgvPool).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private OpenFileDialog ofdBin;
        private Button buttonBin;
        private DataGridView dgvPool;
        private DataGridViewTextBoxColumn firstName;
        private DataGridViewTextBoxColumn lastName;
        private DataGridViewTextBoxColumn nickName;
        private DataGridViewTextBoxColumn soldierClass;
        private DataGridViewTextBoxColumn gender;
        private DataGridViewTextBoxColumn nationality;
        private Button buttonSave;
        private SaveFileDialog sfdbin;
    }
}
