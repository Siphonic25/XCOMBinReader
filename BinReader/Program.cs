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
                        //pool for storing characters
                        Pool pool = new();

                        //each soldier has one instance of "strFirstName", so use this as separating point
                        int nextSoldier = FindString(reader, "strFirstName");
                        
                        //when nextSoldier returns 1 we've ran out of stuff in the bin
                        while (nextSoldier != 1)
                        {
                            Soldier soldier = new();

                            //already at FirstName, find and save it
                            FindString(reader, "StrProperty");
                            soldier.FirstName = ReadData(reader);
                            
                            //then LastName
                            FindString(reader, "strLastName");
                            FindString(reader, "StrProperty");
                            soldier.LastName = ReadData(reader);
                            
                            //then NickName
                            FindString(reader, "strNickName");
                            FindString(reader, "StrProperty");
                            soldier.NickName = ReadData(reader);
                            
                            //then SoldierClass
                            FindString(reader, "ClassTemplateName");
                            FindString(reader, "NameProperty");
                            soldier.SoldierClass = ReadData(reader);

                            //then Gender
                            FindString(reader, "iGender");
                            FindString(reader, "IntProperty");
                            soldier.Gender = ReadGender(reader);

                            //then finally Nationality
                            FindString(reader, "nmFlag");
                            FindString(reader, "NameProperty");
                            soldier.Nationality = ReadData(reader);

                            //add soldier to pool and check if there's still a soldier to do
                            pool.AddSoldier(soldier);
                            nextSoldier = FindString(reader, "strFirstName");
                        }

                        //print the pool baby
                        pool.PrintPool();

                        //for now, this is bunk newline code that exists because my ass doesn't want to get doxxed by the end-of-console info
                        Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                    }
                }
            }

            //the file does not exist; throw up an error message
            else
            {
                Console.WriteLine("ERROR: The file you have specified does not exist.");
                Console.ReadLine();
            }
        }

        /* designed just for reading gender: reads until the *second* piece of non-zero data, and grabs it
         * if 1, it returns "Male", if 0, it returns "Female", else it returns "ERROR"
         */
        private static string ReadGender(BinaryReader reader)
        {
            //need to skip the first bit of data (usually an 04)
            int counter = 0;

            while (true)
            {
                //peek the next character
                int nextChar = (int)reader.PeekChar();
                //if it's nonzero, increment counter
                if (nextChar != 0)
                {
                    counter++;

                    //if counter is now 2, we're on our second bit of data, so nextChar is our desired data
                    if (counter == 2)
                    {
                        //if 1, male
                        if (nextChar == 1) { return "Male"; }

                        //else if 2, female
                        else if (nextChar == 2) { return "Female"; }

                        //else oops
                        else { return "Error"; }
                    }
                }

                //there are no more characters available; we've hit end-of-file (oops).
                else if (nextChar == -1)
                {
                    return "ERROR";
                }

                //consume a character to keep progressing
                reader.ReadChar();
            }
        }

        /* skip all characters lower than the lowest valid ASCII character (i.e. '!')
         * then once a workable character is hit, keep going until first non-valid character, then return the read data
         */
        private static string ReadData(BinaryReader reader)
        {
            //lowest possible value for me to give a damn tbh
            const int minVal = 32;

            while (true)
            {
                //peek the next character
                int nextChar = (int)reader.PeekChar();
                //if it's greater than the lowest value of a valid UTF-8 code (i.e. at or higher than 32);
                if (nextChar >= minVal)
                {
                    List<char> chars = [];

                    //start reading data into a char buffer until another below-min character is found
                    while ((int)reader.PeekChar() >= 32) { chars.Add(reader.ReadChar()); }

                    //once complete, convert buffer to a string and return
                    string str = new(chars.ToArray());
                    return str;
                }

                //there are no more characters available; we've hit end-of-file (oops).
                else if (nextChar == -1) { return "ERROR"; }

                //consume a character to keep progressing
                reader.ReadChar();
            }
        }

        /* reads until a specific string has been located in the file
         * returns 0 if the string has been found and 1 if we've hit end-of-file
         */
        private static int FindString(BinaryReader reader, string str)
        {
            //keep going until the string has been found
            while (true)
            {
                //use FindChar with the first character of the requested string
                //converts the char to its ASCII format by casting to an int (pretty neat tbh)
                int charCode = (int)str[0];
                int check = FindCharacter(reader, charCode);

                //if the return is 0, character has been found
                if (check == 0)
                {
                    //read the following [STRING LENGTH] bytes into a string (char array then converted)
                    char[] data = new char[str.Length];
                    reader.Read(data, 0, data.Length);
                    string readStr = new(data);

                    //compare strings; if they match, return it, if not keep going
                    if (readStr.Equals(str)) { return 0; }
                }

                //else there's been an error (we've hit end-of-file) so return error message
                else { return 1; }
            }
        }

        /* reads until a desired character (given as their UTF-8 hex code in decimal) is found, or the file ends
         * returns a 1 if the end of the file has been hit and a 0 otherwise
         */
        private static int FindCharacter(BinaryReader reader, int character)
        {
            //while the reader has not hit end-of-file
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                //PeekChar will occasionally just crash for reasons that are incomprehensible to me
                //so try catch block time
                try
                {
                    //peek the next character
                    int nextChar = reader.PeekChar();

                    //if it's the desired character, end the loop and return 0
                    if (nextChar == character) { return 0; }

                    //else read the character and loop again
                    else { reader.ReadByte(); }
                }

                //if we've crashed on a character, skip it
                catch
                {
                    reader.ReadByte();
                }
            }

            //while loop has broken meaning we've hit end-of-file
            return 1;
        }
    }
}