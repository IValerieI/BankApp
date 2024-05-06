namespace BankApp
{
    public class Bank
    {
        private Dictionary<string, decimal> moneyBase;

        public Bank()
        {
            moneyBase = new Dictionary<string, decimal>();

            moneyBase.Add("rubles", 1000000000);
            moneyBase.Add("dollars", 1000000000);
            moneyBase.Add("euros", 1000000000);
            moneyBase.Add("pounds", 1000000000);
        }

        public void IncreaseMoney(decimal amount, string currency)
        {
            if (moneyBase.ContainsKey(currency))
            {
                if (amount > 0)
                {
                    moneyBase[currency] += amount;
                }

            }

        }

        public void DecreaseMoney(decimal amount, string currency)
        {
            if (moneyBase.ContainsKey(currency))
            {
                if (amount > 0 && (moneyBase[currency] - amount >= 0))
                {
                    moneyBase[currency] -= amount;
                }

            }

        }

        public void GetInfo()
        {
            foreach (KeyValuePair<string, decimal> entry in moneyBase)
            {
                Console.WriteLine(entry.Key + " " + entry.Value);
            }
        }

        public List<string> GetCurrencies()
        {
            return moneyBase.Keys.ToList();
        }


    }
}
