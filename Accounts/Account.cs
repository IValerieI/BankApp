namespace BankApp
{
    public class Account
    {
        private decimal balance;
        private string currency;
        private int id;

        public Account(string currency)
        {
            balance = 0;
            this.currency = currency;
        }

        public void IncreaseBalance(decimal amount)
        {
            if (amount > 0)
            {
                balance += amount;
            }
        }

        public void DecreaseBalance(decimal amount)
        {
            if (amount > 0 && (balance - amount >= 0))
            {
                balance -= amount;
            }
        }

        public int GetId()
        {
            return id;
        }

        public void SetId(int id)
        {
            this.id = id;
        }

        public decimal GetBalance()
        {
            return balance;
        }

        public string GetCurrency()
        {
            return currency;
        }

        public override string ToString()
        {
            return "Id: " + id +
                   "\nBalance: " + balance +
                   "\nCurrency: " + currency;
        }



    }
}
