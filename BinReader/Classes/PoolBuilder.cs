using System.Text;

namespace BinReader
{
    /// <summary>
    /// Class for the purposes of constructing a Pool from a .bin file.
    /// </summary>
    public class PoolBuilder
    {
        //VARIABLES//

        private List<Soldier> pool = [];    //the list of soldiers pulled from the pool


        //PROPERTIES//

        public List<Soldier> Pool
        {
            get { return pool; } //the pool isn't meant to be modified directly
        }


        //FUNCTIONS//

        /// <summary>
        /// Construct the pool from the provided file.
        /// </summary>
        /// <param name="filePath">The path of the file to build a pool from.</param>
        public void BuildPoolFromFile(string filePath)
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

                //with the pool complete, delete the duplicates from it
                RemoveDuplicates();
            }

            //the file does not exist; throw up an error message
            else
            {
                Console.WriteLine("ERROR: The file you have specified does not exist.");
            }
        }

        //Goal: for whatever reason, there's a whole bunch of duplicates in any given pool
        //delete those

        //currently defining a duplicate as two characters with idential first, last, and nicknames.
        private void RemoveDuplicates()
        {
            //the time complexity on this is gonna be a bitch

            //for every soldier in the pool
            for (int i = 0; i < pool.Count; i++)
            {
                //increment through the following soldiers
                for (int j = i + 1; j < pool.Count; j++)
                {
                    //compare first, last, then nicknames
                    //if they're not equal at any point, move onto next soldier
                    if (String.Equals(pool[i].FirstName, pool[j].FirstName))
                    {
                        if (String.Equals(pool[i].LastName, pool[j].LastName))
                        {
                            if (String.Equals(pool[i].NickName, pool[j].NickName))
                            {
                                //we've found a duplicate, so remove it
                                //then *decrement* j, since all subsequent indexes will be lowered by 1
                                //and we want to avoid skipping whichever soldier is now in j's place
                                pool.RemoveAt(j);
                                j--;
                            }
                        }
                    }
                }
            }
        }

        //Functions to be added
        //A FormatData function to do formatting on the read data
        //a ReadBio version of ReadData (skips until printable character, then reads until an 05 byte)




        /// <summary>
        /// Variant of ReadData for reading gender. Reads until the *second* piece of nonzero data,
        /// converts it into a gender, and returns it. This is because gender has data prior to the gender marker,
        /// which breaks ReadData; and gender needs to be converted into an actual gender.
        /// </summary>
        /// <param name="reader">The BinaryReader processing the file.</param>
        /// <returns>"Male", "Female", or "Error".</returns>
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
                        //1 is male, 2 is female, anything else is an error
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

        /// <summary>
        /// Reads a continuous string of data from the .bin file. Skips all byte values lower than the lowest printable
        /// ASCII character (i.e. '!'), then keeps reading once a printable character is hit until the first non-printable character.
        /// </summary>
        /// <param name="reader">The BinaryReader processing the file.</param>
        /// <returns>The string of data read from the bin.</returns>
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
                    string str = new([.. chars]);
                    return str;
                }

                //there are no more characters available; we've hit end-of-file (oops).
                else if (nextChar == -1) { return "ERROR"; }
            }
        }

        /// <summary>
        /// Reads until a specific string has been located in the file, and moves the BinaryReader past that string.
        /// </summary>
        /// <param name="reader">The BinaryReader processing the file.</param>
        /// <param name="str">The string to be located.</param>
        /// <returns>0 if the string has been found and read, 1 if we've hit end-of file.</returns>
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

        /// <summary>
        /// Reads until a desired character (given as their UTF-8 hex code in decimal) is found, or the file ends.
        /// </summary>
        /// <param name="reader">The BinaryReader processing the file.</param>
        /// <param name="character">The code of the character to be located.</param>
        /// <returns>1 if the end of the file has been hit, 0 otherwise.</returns>
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
