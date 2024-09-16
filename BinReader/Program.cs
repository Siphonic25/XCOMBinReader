using System;
using System.IO;
using System.Text;

namespace BinReader
{
    internal class Program
    {
        /*list of goals:
         * Step 1: Be able to read and reoutput a .bin file (Complete)
         * Step 2: Output personal information into a nice table format
         * Step 3: Output this information into a .txt file
         * Step 4: exeify this
         * (the goal is an .exe where you run it in a folder with .bin files, and it returns .txtified versions of all of them)
         */
        private static void Main(string[] args)
        {
            //get the file name
            //for now it's a const but will need to be a full variable later, that the user enters
            const string fileName = "Siph_A_Lotta_Weasels.bin";

            //if the file in question exists
            if (File.Exists(fileName))
            {
                //open the file
                using (var stream = File.Open(fileName, FileMode.Open))
                {
                    //open a BinaryReader for the file
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        //reads until end of file because an error will be thrown then
                        //I am aware deliberately triggering an error is not a wise idea, I do not care
                        try
                        {
                            while(true)
                            {
                                Console.Write(Convert.ToChar(reader.ReadByte()));
                            }
                        }
                        //end of file section
                        catch
                        {
                            Console.WriteLine("\nEnd of file.");
                        }
                        Console.ReadLine();
                    }
                }
            }

            //the file does not exist; throw up an error message
            //write a proper error message
            else
            {
                Console.WriteLine("ERROR: The file you have specified does not exist.");
                Console.ReadLine();
            }
        }
    }
}