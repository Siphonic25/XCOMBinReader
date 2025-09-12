namespace BinReader
{
    /// <summary>
    /// A representation of the entire character pool and all its contents.
    /// </summary>
    public class Pool
    {
        //you know what, I don't think this class is doing anything meaningful now
        //could merge it into PoolBuilder and replace all instances of it with List<Soldier>


        /// <value>
        /// A list of all the Soldiers in the pool
        /// </value>
        private List<Soldier> soldiers = [];

        //fetching the list of soldiers
        public List<Soldier> Soldiers
        {
            get { return soldiers; }
        }


        /// <summary>
        /// Does exactly what it says on the tin (adds a Soldier to the soldiers list).
        /// </summary>
        /// <param name="soldier">The Soldier object to add.</param>
        public void AddSoldier(Soldier soldier)
        {
            soldiers.Add(soldier);
        }

        /// <summary>
        /// Formats and prints the pool's contents to the console. Mainly used for debugging the printing process,
        /// as printing logic is otherwise identical to PrintPoolToConsole()
        /// </summary>
        public void PrintPoolToConsole()
        {
            Console.WriteLine("FIRST NAME | LAST NAME |  NICKNAME   |   CLASS   |GENDER|  NATIONALITY  ");
            Console.WriteLine("-----------|-----------|-------------|-----------|------|---------------");

            foreach (Soldier soldier in soldiers)
            {
                Console.WriteLine(soldier.FirstName.PadRight(11) + "|"
                    + soldier.LastName.PadRight(11) + "|"
                    + soldier.NickName.PadRight(13) + "|"
                    + soldier.SoldierClass.PadRight(11) + "|"
                    + soldier.Gender.PadRight(6) + "|"
                    + soldier.Nationality);
            }
        }

        /// <summary>
        /// Formats and prints the pool's contents to a given file.
        /// </summary>
        /// <param name="file">File path of the .bin file to read. If it currently doesn't exist, it is created.</param>
        public void PrintPoolToFile(string file)
        {
            ///This function performs editing on the file name. Feel that this isn't the function's job.///

            //trim the file path so it points to a .txt instead of a .bin
            string filePath = file.Remove(file.Length - 3, 3) + "txt";

            //if the file does exist, kill it (sorry file)
            if (File.Exists(filePath)) { File.Delete(filePath); }

            //create and open file
            using (FileStream fs = File.Create(filePath))
            {
                //create StreamWriter for it
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("FIRST NAME | LAST NAME |  NICKNAME   |   CLASS   |GENDER|  NATIONALITY  ");
                    sw.WriteLine("-----------|-----------|-------------|-----------|------|---------------");

                    foreach (Soldier soldier in soldiers)
                    {
                        sw.WriteLine(soldier.FirstName.PadRight(11) + "|"
                            + soldier.LastName.PadRight(11) + "|"
                            + soldier.NickName.PadRight(13) + "|"
                            + soldier.SoldierClass.PadRight(11) + "|"
                            + soldier.Gender.PadRight(6) + "|"
                            + soldier.Nationality);
                    }
                }
            }
        }
    }
}
