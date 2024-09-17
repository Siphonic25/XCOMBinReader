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
                using (FileStream stream = File.Open(fileName, FileMode.Open))
                {
                    //open a BinaryReader for the file
                    using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        //perform findCharacter until the character 'E' (69) is the next one, then print it

                        //int check = FindCharacter(reader, 69);
                        //Console.WriteLine(Convert.ToChar(reader.ReadByte()));

                        Console.WriteLine(FindString(reader, "ExtraDatas"));
                        Console.WriteLine(FindString(reader, "Bingus"));

                        //reads until end of file because an error will be thrown then
                        //I am aware deliberately triggering an error is not a wise idea, I do not care
                        //int i = 0;
                        //try
                        //{
                            //while(i < 100)
                            //{
                                //Console.WriteLine(reader.ReadByte().ToString("X"));
                                //FindCharacter(reader, 1);
                                //i++;
                            //}
                        //}
                        //end of file section
                        //catch
                        //{
                            //Console.WriteLine("\nEnd of file.");
                        //}
                        //Console.ReadLine();

                        //for now, this is bunk newline code
                        Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
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

        //reads the filestream until a specific string has been located
        //could convert this to int returning like FindChararacter?
        private static string FindString(BinaryReader reader, string str)
        {
            //keep going until the string has been found
            while (true)
            {
                //use FindChar with the first character of the requested string
                //converts the char to its ASCII format by casting to an int
                int charCode = (int)str[0];
                int check = FindCharacter(reader, charCode);

                //if the return is 0, character has been found
                if (check == 0)
                {
                    //read the following [STRING LENGTH] bytes into a string (char array then converted)
                    char[] data = new char[str.Length];
                    reader.Read(data, 0, data.Length);
                    string readStr = new string(data);

                    //compare strings; if they match, return it, if not keep going
                    if (readStr.Equals(str))
                    {
                        return readStr;
                    }

                    //else there's been an error (we've hit end-of-file) so return error message
                    else
                    {
                        return "ERROR";
                    }
                }
            }
        }


        //reads the filestream until a desired character (given as their UTF-8 hex code in decimal) is found
        //returns a 1 if the end of the file has been hit and a 0 otherwise
        //potential improvement: let character be an actual character and convert to a proper UTF-8 code in-function
        private static int FindCharacter(BinaryReader reader, int character)
        {
            //while the reader has not hit end-of-file
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                //peek the next character
                int nextChar =  reader.PeekChar();

                //if it's the desired character, end the loop and hand back control
                if (nextChar == character)
                {
                    return 0;
                }

                //else read the character and loop again
                else
                {
                    reader.ReadByte();
                }
            }

            //while loop has broken meaning we've hit end-of-file
            return 1;
        }
    }
}