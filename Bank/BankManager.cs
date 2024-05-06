namespace BankApp
{
    public class BankManager
    {
        private Bank bank;
        public BankManager(Bank bank)
        {
            this.bank = bank;

        }

        public void ManageBank()
        {
            Console.WriteLine("1 - Bank info");
            Console.WriteLine("M - Return to main menu");

            string option = "0";
            while (option != "M")
            {
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        bank.GetInfo();
                        break;
                    case "M":
                        Console.WriteLine("Returned to main menu");
                        break;
                    default:
                        Console.WriteLine("Wrong option. Choose option from list above.");
                        break;
                }
            }
        }
    }
}
