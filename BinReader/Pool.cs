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
            Console.WriteLine("POOL [FILE NAME]\n\n");
            Console.WriteLine("FIRST NAME | LAST NAME | NICKNAME  |   CLASS   |GENDER|  NATIONALITY  ");
            Console.WriteLine("-----------|-----------|-----------|-----------|------|---------------");
            //now print all the soldier shit
        }
    }
}