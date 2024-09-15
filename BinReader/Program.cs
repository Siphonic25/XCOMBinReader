namespace BinReader
{
    internal class Program
    {
        /*list of goals:
         * Step 1: Be able to read and reoutput a .bin file
         * Step 2: Output personal information into a nice table format
         * Step 3: Output this information into a .txt file
         * Step 4: exeify this
         * (the goal is an .exe where you run it in a folder with .bin files, and it returns .txtified versions of all of them)
         */
        private static void Main(string[] args)
        {
            //short term objective (Step 1):
            //open a .bin file, read each character individually, and print to console until the file has ended
            Console.WriteLine("Hello, World!");
            Console.ReadLine();

            using (BinaryReader br = new BinaryReader(File.Open("/Siph_A_Lotta_Weasels.bin")))
            {

            }
        }
    }
}