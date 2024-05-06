namespace BankApp
{
    public class ClientManager
    {
        private AccountManager accountManager;
        private DepositManager depositManager;
        private Dictionary<int, Client> clients;
        private int lastClientId = 0;

        public ClientManager(AccountManager accountManager, DepositManager depositManager)
        {
            clients = new Dictionary<int, Client>();
            this.accountManager = accountManager;
            this.depositManager = depositManager;
        }

        public void ManageClients()
        {
            Console.WriteLine("1 - Create client");
            Console.WriteLine("2 - Edit client");
            Console.WriteLine("3 - Delete client");
            Console.WriteLine("4 - Show all clients");
            Console.WriteLine("5 - Show client with id");
            Console.WriteLine("6 - Show balance of client with id");
            Console.WriteLine("M - Return to main menu");
            string option = "0";
            while (option != "M")
            {
                int id = 0;
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Console.WriteLine("Write firstname");
                        string firstname = Console.ReadLine();
                        Console.WriteLine("Write lastname");
                        string lastname = Console.ReadLine();
                        Console.WriteLine("Write age");
                        int age = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write city");
                        string city = Console.ReadLine();
                        AddClient(firstname, lastname, age, city);
                        Console.WriteLine("Client created");
                        break;
                    case "2":
                        Console.WriteLine("Write clientId");
                        id = Convert.ToInt32(Console.ReadLine());
                        EditClient(id);
                        Console.WriteLine("Client edited");
                        break;
                    case "3":
                        Console.WriteLine("Write clientId");
                        id = Convert.ToInt32(Console.ReadLine());
                        accountManager.HandleClientDeletion(id);
                        depositManager.HandleClientDeletion(id);
                        DeleteClient(id);
                        Console.WriteLine("Client deleted");
                        break;
                    case "4":
                        Console.WriteLine("All clients");
                        PrintAllClientsInfo();
                        break;
                    case "5":
                        Console.WriteLine("Client with id");
                        Console.WriteLine("Write clientId");
                        id = Convert.ToInt32(Console.ReadLine());
                        PrintClientInfo(id);
                        break;
                    case "6":
                        Console.WriteLine("Balance of client with id");
                        Console.WriteLine("Write clientId");
                        id = Convert.ToInt32(Console.ReadLine());
                        PlintBankAccountInfo(id);
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
        private void DeleteClient(int id)
        {
            if (CheckIfClientExists(id))
            {
                clients.Remove(id);
            }

        }

        public void AddClient(string firstname, string lastname, int age, string city)
        {
            int id = lastClientId + 1;
            lastClientId += 1;
            Client client = new Client(firstname, lastname, age, city);
            client.SetId(id);

            clients.Add(id, client);

        }

        private void EditClient(int id)
        {
            if (CheckIfClientExists(id))
            {
                Client client = clients[id];
                Console.WriteLine("Write firstname");
                client.SetFirstname(Console.ReadLine());
                Console.WriteLine("Write lastname");
                client.SetLastname(Console.ReadLine());
                Console.WriteLine("Write age");
                client.SetAge(Convert.ToInt32(Console.ReadLine()));
                Console.WriteLine("Write city");
                client.SetCity(Console.ReadLine());

            }
        }

        private void PrintClientInfo(int id)
        {
            if (CheckIfClientExists(id))
            {
                Console.WriteLine(clients[id].ToString() + "\n");
            }


        }

        private void PrintAllClientsInfo()
        {
            if (clients.Count() > 0)
            {
                foreach (KeyValuePair<int, Client> client in clients)
                {
                    PrintClientInfo(client.Key);
                }
            }
            else
            {
                Console.WriteLine("Number of clients is 0");
            }

        }

        private void PlintBankAccountInfo(int id)
        {
            if (CheckIfClientExists(id))
            {
                List<Account> accounts = accountManager.GetClientAccounts(id);
                if (accounts != null)
                {
                    foreach (Account account in accounts)
                    {
                        Console.WriteLine("Balance for account " + account.GetId() +
                                          " is " + account.GetBalance() + " " + account.GetCurrency());
                    }
                }
                else
                {
                    Console.WriteLine("Client with id " + id + " doesn't have bank accounts");
                }
            }


        }

        private bool CheckIfClientExists(int id)
        {
            if (clients.ContainsKey(id))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Client with id " + id + " doesn't exist");
                return false;
            }
        }

        public Dictionary<int, Client> GetClients()
        {
            return clients;
        }
    }
}

