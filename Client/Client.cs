namespace BankApp
{
    public class Client
    {
        private int id;
        private string firstname;
        private string lastname;
        private int age;
        private string city;

        public Client(string firstname, string lastname, int age, string city)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.age = age;
            this.city = city;

        }

        public void SetId(int id) { this.id = id; }
        public void SetFirstname(string firstname) { this.firstname = firstname; }
        public void SetLastname(string lastname) { this.lastname = lastname; }
        public void SetAge(int age) { this.age = age; }
        public void SetCity(string city) { this.city = city; }

        public override string ToString()
        {
            return "Id: " + id +
                   "\nFirstname: " + firstname +
                   "\nLastname: " + lastname +
                   "\nAge: " + age +
                   "\nCity: " + city;
        }



    }
}
