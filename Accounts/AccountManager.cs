namespace BankApp
{
    public class AccountManager
    {
        private Bank bank;
        private Converter converter;
        private Dictionary<int, List<Account>> allAccounts;
        private int lastAccountId = 0;

        public AccountManager(Bank bank, Converter converter)
        {
            allAccounts = new Dictionary<int, List<Account>>();
            this.converter = converter;
            this.bank = bank;

        }

        public void ManageAccounts()
        {
            Console.WriteLine("1 - Open account");
            Console.WriteLine("2 - Close account");
            Console.WriteLine("3 - Add money to account");
            Console.WriteLine("4 - Withdraw money from account");
            Console.WriteLine("5 - Transfer money to another account");
            Console.WriteLine("6 - Show accounts of all clients");
            Console.WriteLine("7 - Show accounts of client with id");
            Console.WriteLine("M - Return to main menu");
            Console.WriteLine("Currencies: rubles, dollars, euros, pounds");

            int clientId;
            string currency;
            int accountId;
            decimal amount;

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
                        OpenAccount(clientId, currency);
                        Console.WriteLine("Account opened");
                        break;
                    case "2":
                        Console.WriteLine("Write clientId");
                        clientId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write accountId");
                        accountId = Convert.ToInt32(Console.ReadLine());
                        DeleteAccount(clientId, accountId);
                        Console.WriteLine("Account closed");
                        break;
                    case "3":
                        Console.WriteLine("Write clientId");
                        clientId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write accountId");
                        accountId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write amount");
                        amount = Convert.ToDecimal(Console.ReadLine());
                        AddMoney(clientId, accountId, amount);
                        Console.WriteLine("Money added");
                        break;
                    case "4":
                        Console.WriteLine("Write clientId");
                        clientId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write accountId");
                        accountId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write amount");
                        amount = Convert.ToDecimal(Console.ReadLine());
                        WithdrawMoney(clientId, accountId, amount);
                        Console.WriteLine("Balance decreased");
                        break;
                    case "5":
                        Console.WriteLine("Write clientId");
                        clientId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write accountIdFrom");
                        int accountIdFrom = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write accountIdTo");
                        int accountIdTo = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write amount");
                        amount = Convert.ToDecimal(Console.ReadLine());
                        TransferMoney(clientId, accountIdFrom, accountIdTo, amount);
                        Console.WriteLine("Transfered money");
                        break;
                    case "6":
                        Console.WriteLine("Accounts of all clients");
                        ShowAllAccountsAndClients();
                        break;
                    case "7":
                        Console.WriteLine("Accounts of client with id");
                        Console.WriteLine("Write clientId");
                        clientId = Convert.ToInt32(Console.ReadLine());
                        ShowAccountsByClientId(clientId);
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
            if (allAccounts.ContainsKey(id))
            {
                allAccounts.Remove(id);
            }

        }

        private void DeleteAccount(int clientId, int accountId)
        {
            if (CheckIfAccountExixts(clientId, accountId))
            {
                Account account = GetAccount(clientId, accountId);
                decimal balance = account.GetBalance();
                string currency = account.GetCurrency();

                List<Account> clientAccounts = allAccounts[clientId];
                clientAccounts.Remove(account);
            }

        }

        public void AddMoney(int clientId, int accountId, decimal amount)
        {
            if (CheckIfAccountExixts(clientId, accountId))
            {
                Account account = GetAccount(clientId, accountId);
                account.IncreaseBalance(amount);

                string currency = account.GetCurrency();
                bank.IncreaseMoney(amount, currency);
            }

        }

        public void WithdrawMoney(int clientId, int accountId, decimal amount)
        {
            if (CheckIfAccountExixts(clientId, accountId))
            {
                Account account = GetAccount(clientId, accountId);
                account.DecreaseBalance(amount);

                string currency = account.GetCurrency();
                bank.DecreaseMoney(amount, currency);
            }


        }

        public void TransferMoney(int clientId, int accountIdFrom, int accountIdTo, decimal amount)
        {
            if (CheckIfAccountExixts(clientId, accountIdTo))
            {
                Account accountFrom = GetAccount(clientId, accountIdFrom);
                Account accountTo = GetAccount(clientId, accountIdTo);

                if (accountFrom.GetCurrency().Equals(accountTo.GetCurrency()))
                {
                    accountFrom.DecreaseBalance(amount);
                    accountTo.IncreaseBalance(amount);
                }
                else
                {
                    string cufrrencyFrom = accountFrom.GetCurrency();
                    string cufrrencyTo = accountTo.GetCurrency();

                    accountFrom.DecreaseBalance(amount);
                    bank.DecreaseMoney(amount, cufrrencyFrom);

                    decimal newAmount = 0;
                    switch (cufrrencyFrom)
                    {
                        case "rubles":
                            newAmount = converter.ConvertRublesTo(amount, cufrrencyTo);
                            break;
                        case "dollars":
                            newAmount = converter.ConvertDollarsTo(amount, cufrrencyTo);
                            break;
                        case "euros":
                            newAmount = converter.ConvertEurosTo(amount, cufrrencyTo);
                            break;
                        case "pounds":
                            newAmount = converter.ConvertPoundsTo(amount, cufrrencyTo);
                            break;
                        default:
                            Console.WriteLine("Wrong option. Choose option from list above.");
                            break;
                    }
                    accountTo.IncreaseBalance(newAmount);
                    bank.IncreaseMoney(amount, cufrrencyTo);
                }



            }
        }

        public List<Account> GetClientAccounts(int clientId)
        {
            if (allAccounts.ContainsKey(clientId))
            {
                return allAccounts[clientId];
            }
            else
            {
                return null;
            }

        }

        public void OpenAccount(int clientId, string currency)
        {
            if (bank.GetCurrencies().Contains(currency))
            {
                Account newAccount = new Account(currency);
                newAccount.SetId(lastAccountId);
                lastAccountId += 1;
                if (!allAccounts.ContainsKey(clientId))
                {
                    allAccounts.Add(clientId, new List<Account> { newAccount });
                }
                else
                {
                    allAccounts[clientId].Add(newAccount);
                }
            }



        }

        private void ShowAllAccountsAndClients()
        {
            if (allAccounts.Count > 0)
            {
                foreach (KeyValuePair<int, List<Account>> entry in allAccounts)
                {
                    Console.WriteLine("ClientId: " + entry.Key);
                    foreach (Account account in entry.Value)
                    {
                        Console.WriteLine(account.ToString());
                    }
                }
            }
            else
            {
                Console.WriteLine("Number of accounts is 0");
            }

        }

        private void ShowAccountsByClientId(int clientId)
        {
            if (allAccounts.ContainsKey(clientId))
            {
                List<Account> clientAccounts = allAccounts[clientId];
                foreach (Account account in clientAccounts)
                {
                    Console.WriteLine(account.ToString());
                }
            }
            else
            {
                Console.WriteLine("Number of accounts is 0");
            }

        }



        private Account GetAccount(int clientId, int accountId)
        {
            return allAccounts[clientId].Where(a => a.GetId() == accountId).First();

        }

        private bool CheckIfAccountExixts(int clientId, int accountId)
        {
            if (allAccounts.ContainsKey(clientId) && allAccounts[clientId].Any(a => a.GetId() == accountId))
            {
                return true;
            }
            else
            {
                Console.WriteLine("The bank doesn't have account clientId " + clientId + " and accountId " + accountId);
                return false;
            }

        }
    }
}
