namespace BankApp
{
    public class Program
    {
        static Bank bank = new Bank();
        static Converter converter = new Converter();
        static BankManager bankManager = new BankManager(bank);
        static DepositManager depositManager = new DepositManager(bank);
        static AccountManager accountManager = new AccountManager(bank, converter);
        static ClientManager clientManager = new ClientManager(accountManager, depositManager);

        static void Main(string[] args)
        {
            Seed();

            Console.WriteLine("1 - Manage clients (create, delete, etc) ");
            Console.WriteLine("2 - Manage bank accounts (open, transfer money between accounts)");
            Console.WriteLine("3 - Manage deposits (open, calculate interest)");
            Console.WriteLine("4 - Bank info");
            Console.WriteLine("Exit - Exit app");

            string option = "Start";
            while (!option.Equals("Exit"))
            {
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        clientManager.ManageClients();
                        break;
                    case "2":
                        accountManager.ManageAccounts();
                        break;
                    case "3":
                        depositManager.ManageDeposits();
                        break;
                    case "4":
                        bankManager.ManageBank();
                        break;
                    case "Exit":
                        Console.WriteLine("You've log out.");
                        break;
                    default:
                        Console.WriteLine("Wrong option. Choose option from list above.");
                        break;
                }


            }
        }

        static void Seed()
        {
            clientManager.AddClient("Ivan", "Ivanov", 25, "Moscow");
            clientManager.AddClient("Sasha", "Petrova", 56, "Voronezh");
            clientManager.AddClient("Vlad", "Sidorov", 37, "St. Petersburg");

            accountManager.OpenAccount(1, "rubles");
            accountManager.OpenAccount(1, "euros");
            accountManager.AddMoney(1, 0, 5000);
            accountManager.TransferMoney(1, 0, 1, 1000);

            depositManager.OpenDeposit(2, "pounds", 100000, 6, 12);
            depositManager.CalcInterest(0, 0, 2);
        }

    }
}
