using System.Diagnostics;
using System.Security;
using System.Windows.Forms;

namespace BinReader
{
    public partial class HomeScreen : Form
    {
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
                    using (Stream str = ofdBin.OpenFile())
                    {
                        //this is where all the processing of the file needs to take place
                        //for now we open it in notepad
                        Process.Start("notepad.exe", filePath);
                    }
                }

                //something's gone wrong; error handling
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }
    }
}
