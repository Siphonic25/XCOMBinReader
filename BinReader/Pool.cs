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

        //print out the entire contents of the pool (for now it's a seat warmer, need to fill out)
        //will also need to be renovated/replaced by a PrintPoolToFile command
        public void PrintPool()
        {
            //Console.WriteLine("POOL [FILE NAME]\n\n");    //straight up don't feel like doing this rn
            Console.WriteLine("FIRST NAME | LAST NAME |  NICKNAME   |   CLASS   |GENDER|  NATIONALITY  ");
            Console.WriteLine("-----------|-----------|-------------|-----------|------|---------------");
            //now print all the soldier details
            foreach (Soldier soldier in soldiers)
            {
                //awful
                Console.WriteLine(soldier.FirstName.PadRight(11) + "|" 
                    + soldier.LastName.PadRight(11) + "|"
                    + soldier.NickName.PadRight(13) + "|"
                    + soldier.SoldierClass.PadRight(11) + "|"
                    + soldier.Gender.PadRight(6) + "|"
                    + soldier.Nationality);
            }
        }

        //as above, but prints to a newly created text file with the given name
        public void PrintPoolToFile
    }
}