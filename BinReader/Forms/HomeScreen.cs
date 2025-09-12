using System.Diagnostics;
using System.Security;

namespace BinReader
{
    public partial class HomeScreen : Form
    {
        PoolBuilder poolBuilder = new();    //setting up a PoolBuilder for handling the provided file.

        public HomeScreen()
        {
            InitializeComponent();
        }

        //roughly covers the opening and selection of a .bin file
        private void buttonBin_Click(object sender, EventArgs e)
        {
            //try and open the file selected
            if (ofdBin.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = ofdBin.FileName;

                    //I'm already doing this in the PoolBuilder file lmao
                    //this is what I get for code retrofitting like crazy
                    //the job of correcting this tech debt will be left as an exercise to the reader
                    //using (Stream str = ofdBin.OpenFile())
                    //{
                        //this is where all the processing of the file needs to take place
                        //start by assigning it to the PoolBuilder and building from there
                        poolBuilder.FilePath = filePath;
                        poolBuilder.BuildPoolFromFile();

                        //populate the DataGridView
                        PopulateDataGridView();
                    //}
                }

                //something's gone wrong; error handling
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        //does exactly what it says on the tin.
        private void PopulateDataGridView()
        {
            //wipe the DGV if it isn't empty already
            dgvPool.Rows.Clear();

            //show it to the user so they can watch the construction if they want :)
            dgvPool.Visible = true;

            //add each soldier to the DGV
            foreach (Soldier soldier in poolBuilder.fetchSoldiers())
            {
                dgvPool.Rows.Add(soldier.SoldierToArray());
            }
        }
    }
}
