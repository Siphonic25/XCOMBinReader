using System;
using System.IO;
using System.Text;

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

            //get the file name
            //for now it's a const but will need to be a full variable later
            const string fileName = "Siph_A_Lotta_Weasels.bin";

            if (File.Exists(fileName))
            {
                using (var stream = File.Open(fileName, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    //using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        Console.WriteLine("he");
                        //Console.WriteLine(reader.Read());
                        //Console.ReadLine();
                        try
                        {
                            //int i = 0, j = 0;
                            while(true)
                            {
                                //if (i < 15)
                                //{
                                    Console.Write(Convert.ToChar(reader.ReadByte()));
                                    //Convert.ToChar(reader.ReadByte());
                                    //i++;
                                //}
                                //else
                                //{
                                //    Console.Write("line " + j + "\n");
                                //    i = 0;
                                //    j++;
                                //}
                            }
                        }
                        catch
                        {
                            Console.WriteLine("End of file");
                        }
                        Console.ReadLine();
                    }
                }
            }

            //write a proper error message
            else
            {
                Console.WriteLine(Directory.GetCurrentDirectory());
                Console.ReadLine();
            }
        }
    }
}