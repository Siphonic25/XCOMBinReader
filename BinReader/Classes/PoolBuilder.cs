using System.Text;

namespace BinReader
{
    //class for the purposes of constructing a Pool from a .bin file
    //depending on complexity, might merge this with the Pool class itself
    public class PoolBuilder
    {
        private List<Soldier> pool = [];    //the list of soldiers pulled from the pool
        private string filePath;            //the path of the .bin file the Poolbuilder is building from


        //Constructors: can build one with or without the filePath initialised
        public PoolBuilder()
        {
            filePath = string.Empty;
        }

        public PoolBuilder(string filePath)
        {
            this.filePath = filePath;
        }


        //Properties
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        public List<Soldier> Pool
        {
            get { return pool; }
        }

        //Functions

        //construct the pool from the provided file
        public void BuildPoolFromFile()
        {
            //check if the provided file is alive
            if (File.Exists(filePath))
            {
                //reinitialise the pool to ensure it's clear
                pool = [];

                //open the file
                using (FileStream stream = File.Open(filePath, FileMode.Open))
                {
                    //open a BinaryReader for the file
                    using (BinaryReader reader = new(stream, Encoding.UTF8, false))
                    {
                        //each soldier has one instance of "strFirstName", so use this as separating point
                        int nextSoldier = FindString(reader, "strFirstName");

                        //when nextSoldier returns 1 we've ran out of stuff in the bin
                        while (nextSoldier != 1)
                        {
                            //construct a soldier and fill its information
                            Soldier soldier = new();

                            //values are saved after an instance of [Str/Name/Int]Property
                            FindString(reader, "StrProperty");
                            soldier.FirstName = ReadData(reader);

                            FindString(reader, "strLastName");
                            FindString(reader, "StrProperty");
                            soldier.LastName = ReadData(reader);

                            FindString(reader, "strNickName");
                            FindString(reader, "StrProperty");
                            soldier.NickName = ReadData(reader);

                            FindString(reader, "ClassTemplateName");
                            FindString(reader, "NameProperty");
                            soldier.SoldierClass = ReadData(reader);

                            FindString(reader, "iGender");
                            FindString(reader, "IntProperty");
                            soldier.Gender = ReadGender(reader);

                            FindString(reader, "nmFlag");
                            FindString(reader, "NameProperty");
                            soldier.Nationality = ReadData(reader);

                            //add soldier to pool and check if there's still a soldier to do
                            pool.Add(soldier);
                            nextSoldier = FindString(reader, "strFirstName");
                        }
                    }
                }
            }

            //the file does not exist; throw up an error message
            else
            {
                Console.WriteLine("ERROR: The file you have specified does not exist.");
            }
        }

        /* Designed just for reading gender: reads until the *second* piece of non-zero data, and grabs it
         * (this is because gender has a nonzero piece of data prior to the gender marker, breaking ReadData)
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
                        if (nextChar == 1) { return "Male"; }
                        else if (nextChar == 2) { return "Female"; }
                        else { return "ERROR"; }
                    }
                }

                //there are no more characters available; we've hit end-of-file (oops).
                else if (nextChar == -1) { return "ERROR"; }

                //consume a character to keep progressing
                reader.ReadChar();
            }
        }

        /* skip all characters lower than the lowest printable ASCII character (i.e. '!')
         * then once a printable character is hit, keep reading until first non-printable character, then return the read data
         */
        private static string ReadData(BinaryReader reader)
        {
            //lowest possible value for me to give a damn tbh
            const int minVal = 32;

            while (true)
            {
                //pull the next character
                int nextChar = reader.ReadByte();

                if (nextChar >= minVal)
                {
                    //list of chars (we'll convert to string later)
                    List<char> chars = [];

                    while (nextChar >= minVal)
                    {
                        //add the read character and read the next one
                        //ReadByte is used because ReadChar occasionally shits itself
                        chars.Add(Convert.ToChar(nextChar));
                        nextChar = reader.ReadByte();
                    }

                    //once complete, convert buffer to a string and return
                    string str = new(chars.ToArray());
                    return str;
                }

                //there are no more characters available; we've hit end-of-file (oops).
                else if (nextChar == -1) { return "ERROR"; }
            }
        }

        /* reads until a specific string has been located in the file
         * returns 0 if the string has been found and read, 1 if we've hit end-of-file
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
