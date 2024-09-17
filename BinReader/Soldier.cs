namespace BinReader
{
    //contains all of a soldier's information for easy printing
    public class Soldier
    {
        private string firstName = "";
        private string lastName = "";
        private string nickName = "";
        private string soldierClass = "";
        private string gender = "";
        private string nationality = "";
        //private string[] bio;        //I have no idea how to do bios right now

        //empty constructor, initialises things
        public Soldier()
        {
        }

        //getters and setters baby
        //we all love some getters and setters
        public string FirstName
        {
            get { return firstName; } 
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string NickName
        {
            get { return nickName; }
            set { nickName = value; }
        }

        public string SoldierClass
        {
            get { return soldierClass; }
            set { soldierClass = value; }
        }

        public string Gender
        {
            get { return gender; }
            set
            {
                //test this boy
                if (value.Equals("1"))
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }
            }
        }

        public string Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }
    }
}