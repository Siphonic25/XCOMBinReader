namespace BinReader
{
    /// <summary>
    /// A representation of the information of a Soldier in XCOM 2.
    /// </summary>
    public class Soldier
    {
        private string firstName = "";      //soldier's first name
        private string lastName = "";       //soldier's last name
        private string nickName = "";       //soldier's callsign (it's spelled this way in the bin file don't @ me)
        private string soldierClass = "";   //soldier's class
        private string gender = "";         //soldier's gender
        private string nationality = "";    //soldier's nationality
        //private string[] bio;             //soldier's bio (I have no idea how to do bios right now)

        //getters and setters baby, we all love some getters and setters
        //god I love Properties
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
            set { gender = value; }
        }

        public string Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }
    }
}
