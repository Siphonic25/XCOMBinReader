using System.Security;

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
                    //build from pool and populate DGV
                    var filePath = ofdBin.FileName;
                    poolBuilder.BuildPoolFromFile(filePath);
                    PopulateDataGridView();
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
    }
}
