namespace BinReader
{
    //a representation of the entire pool containing all of its soldiers
    public class Pool
    {
        private List<Soldier> soldiers = [];    //list of all the soldiers in the pool


        //adds a soldier to the Pool's list
        public void AddSoldier(Soldier soldier)
        {
            soldiers.Add(soldier);
        }


        //prints the pool to the console, mainly used for debugging the actual printing process
        //except for file handling, is otherwise identical to PrintPoolToFile
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

        //as above, but prints to a newly created text file with the given name
        public void PrintPoolToFile(string file)
        {
            //trim the file path so it points to a .txt instead of a .bin
            string filePath = file.Remove(file.Length - 3, 3) + "txt";

            //if the file does exist, kill it (sorry file)
            if (File.Exists(filePath)) { File.Delete(filePath); }

            //create file
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