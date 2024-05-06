namespace BankApp
{
    public class DepositManager
    {
        private Bank bank;
        private Dictionary<int, List<Deposit>> allDeposits;
        private int lastDepositId = 0;

        public DepositManager(Bank bank)
        {
            allDeposits = new Dictionary<int, List<Deposit>>();
            this.bank = bank;

        }

        public void ManageDeposits()
        {
            int clientId;
            string currency;
            int months;
            int depositId;
            decimal amount;
            decimal percent;

            Console.WriteLine("1 - Open deposit");
            Console.WriteLine("2 - Calculate interest");
            Console.WriteLine("3 - Show interest");
            Console.WriteLine("4 - Withdraw interest");
            Console.WriteLine("5 - Show all deposits");
            Console.WriteLine("M - Return to main menu");

            string option = "0";
            while (option != "M")
            {
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Console.WriteLine("Write clientId");
                        clientId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write currency");
                        currency = Console.ReadLine();
                        Console.WriteLine("Write amount");
                        amount = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Write percent");
                        percent = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Write months");
                        months = Convert.ToInt32(Console.ReadLine());
                        OpenDeposit(clientId, currency, amount, percent, months);
                        Console.WriteLine("Deposit opened");
                        break;
                    case "2":
                        Console.WriteLine("Write clientId");
                        clientId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write depositId");
                        depositId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write months");
                        months = Convert.ToInt32(Console.ReadLine());
                        CalcInterest(clientId, depositId, months);
                        Console.WriteLine("Interest calculated");
                        break;
                    case "3":
                        Console.WriteLine("Write clientId");
                        clientId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write depositId");
                        depositId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Interest is");
                        ShowInterest(clientId, depositId);
                        break;
                    case "4":
                        Console.WriteLine("Write clientId");
                        clientId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write depositId");
                        depositId = Convert.ToInt32(Console.ReadLine());
                        WithdrawInterest(clientId, depositId);
                        Console.WriteLine("Now interest of deposit " + depositId + " is 0");
                        break;
                    case "5":
                        ShowAllDepositsAndClients();
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

        public void HandleClientDeletion(int id)
        {
            if (allDeposits.ContainsKey(id))
            {
                allDeposits.Remove(id);
            }

        }

        public void OpenDeposit(int clientId, string currency, decimal amount, decimal percent, int months)
        {
            if (bank.GetCurrencies().Contains(currency))
            {
                Deposit newDeposit = new Deposit(months, percent, amount, currency);
                bank.IncreaseMoney(amount, currency);
                newDeposit.SetId(lastDepositId);
                lastDepositId += 1;
                if (!allDeposits.ContainsKey(clientId))
                {
                    allDeposits.Add(clientId, new List<Deposit> { newDeposit });
                }
                else
                {
                    allDeposits[clientId].Add(newDeposit);
                }

            }
        }

        public void CalcInterest(int clientId, int depositId, int months)
        {
            if (CheckIfDepositExixts(clientId, depositId))
            {
                Deposit deposit = GetDeposit(clientId, depositId);
                if (deposit != null)
                {
                    deposit.CalcInterest(months);
                    Console.WriteLine(deposit.GetInterest());
                }
            }

        }

        private void ShowInterest(int clientId, int depositId)
        {
            if (CheckIfDepositExixts(clientId, depositId))
            {
                Console.WriteLine(GetDeposit(clientId, depositId).GetInterest());
            }
        }

        private void ShowAllDepositsAndClients()
        {
            if (allDeposits.Count > 0)
            {
                foreach (KeyValuePair<int, List<Deposit>> entry in allDeposits)
                {
                    Console.WriteLine("ClientId: " + entry.Key);
                    foreach (Deposit deposit in entry.Value)
                    {
                        Console.WriteLine(deposit.ToString());
                    }
                }
            }
            else
            {
                Console.WriteLine("Number of deposits is 0");
            }

        }

        private void WithdrawInterest(int clientId, int depositId)
        {
            if (CheckIfDepositExixts(clientId, depositId))
            {
                Deposit deposit = GetDeposit(clientId, depositId);
                bank.DecreaseMoney(deposit.GetInterest(), deposit.GetCurrency());
                deposit.WithdrawInterest();
            }

        }

        private Deposit GetDeposit(int clientId, int depositId)
        {
            return allDeposits[clientId].Where(d => d.GetId() == depositId).First();

        }

        private bool CheckIfDepositExixts(int clientId, int depositId)
        {
            if (allDeposits.ContainsKey(clientId) && allDeposits[clientId].Any(d => d.GetId() == depositId))
            {
                return true;
            }
            else
            {
                Console.WriteLine("The bank doesn't have deposit clientId " + clientId + " and depositId " + depositId);
                return false;
            }

        }


    }
}
