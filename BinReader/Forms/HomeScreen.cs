using System.Security;
using System.Text;
using System.Windows.Forms;

namespace BinReader
{
    public partial class HomeScreen : Form
    {
        /// <summary>
        /// The PoolBuilder that handles the file selected by the user.
        /// </summary>
        PoolBuilder poolBuilder = new();


        public HomeScreen()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Covers selecting the .bin file and processing its contents.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBin_Click(object sender, EventArgs e)
        {
            //try and open the file selected
            if (ofdBin.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = ofdBin.FileName;

                    //file handling is currently handled by PoolBuilder.BuildPoolFromFile()
                    //might move it over to here
                    //using (Stream str = ofdBin.OpenFile())
                    //{
                    //build from pool and populate DGV
                    poolBuilder.FilePath = filePath;
                    poolBuilder.BuildPoolFromFile();
                    PopulateDataGridView();
                    //}


                    //show the button for saving now that there's stuff to save.
                    buttonSave.Visible = true;
                }

                //something's gone wrong; error handling
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        /// <summary>
        /// Does exactly what it says on the tin. Populates the DGV with the contents of the PoolBuilder.
        /// </summary>
        private void PopulateDataGridView()
        {
            //wipe the DGV if it isn't empty already
            dgvPool.Rows.Clear();

            //show it to the user so they can watch the construction if they want :)
            dgvPool.Visible = true;

            //add each soldier to the DGV
            foreach (Soldier soldier in poolBuilder.Pool)
            {
                dgvPool.Rows.Add(soldier.SoldierToArray());
            }
        }

        /// <summary>
        /// When clicked, save the contents of the DGV to file (as a .csv).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            //if the dgv has anything to save
            if (dgvPool.Rows.Count > 0)
            {
                //attempt to save the file
                if (sfdbin.ShowDialog() == DialogResult.OK)
                {
                    //if the file already exists, try to delete it
                    if (File.Exists(sfdbin.FileName))
                    {
                        try
                        {
                            File.Delete(sfdbin.FileName);
                        }

                        catch (IOException ex)
                        {
                            MessageBox.Show("Failed to write data to disk: " + ex.Message);
                            return;
                        }
                    }

                    //try to save the contents of the dgv to file
                    try
                    {
                        string[] outputCsv = new string[dgvPool.Rows.Count + 1]; //+ 1 to account for the column headers

                        //add the column headers to the output CSV
                        string columnNames = "";
                        for (int i = 0; i < dgvPool.ColumnCount; i++)
                        {
                            columnNames += dgvPool.Columns[i].HeaderText.ToString() + ",";
                        }
                        outputCsv[0] += columnNames;

                        //add all the rows to the output CSV
                        for (int i = 1; (i - 1) < dgvPool.RowCount; i++)
                        {
                            for (int j = 0; j < dgvPool.ColumnCount; j++)
                            {
                                //look null warning, if it crashes here i'm just gonna have to suffer
                                outputCsv[i] += dgvPool.Rows[i - 1].Cells[j].Value.ToString() + ",";
                            }
                        }

                        File.WriteAllLines(sfdbin.FileName, outputCsv, Encoding.UTF8);
                        MessageBox.Show("File Saved Successfully.", "Info");
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Error :" + ex.Message);
                    }
                }
            }

            else
            {
                MessageBox.Show("The chosen bin is empty and there's nothing to save.", "Notice");
            }
        }
    }
}
