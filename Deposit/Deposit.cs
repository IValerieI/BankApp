namespace BankApp
{
    public class Deposit
    {
        private int id;
        private decimal baseSum;
        private decimal percent;
        private int monthsLeft;
        private string currency;
        private decimal interest;

        public Deposit(int monthsLeft, decimal percent, decimal baseSum, string currency)
        {
            this.monthsLeft = monthsLeft;
            this.percent = percent;
            this.baseSum = baseSum;
            this.interest = 0;
            this.currency = currency;
        }

        public int GetId()
        {
            return id;
        }

        public void SetId(int id)
        {
            this.id = id;
        }

        public void CalcInterest(int months)
        {
            if (months <= monthsLeft)
            {
                decimal monthPercent = percent / 12;
                for (int i = 0; i < months; i++)
                {
                    interest += ((baseSum + interest) / 100) * monthPercent;
                }
                monthsLeft -= months;
            }
            else
            {
                Console.WriteLine("Deposit closed. Balance: " + (baseSum + interest) + ", final interest: " + interest);
            }
        }

        public decimal GetInterest()
        {
            return interest;
        }

        public string GetCurrency()
        {
            return currency;
        }

        public void WithdrawInterest()
        {
            interest = 0; ;
        }

        public override string ToString()
        {
            return "Id: " + id +
                   "\nBase sum: " + baseSum +
                   "\nCurrent interest: " + interest +
                   "\nPercent: " + percent + "%" +
                   "\nMonths left: " + monthsLeft +
                   "\nCurrency: " + currency;
        }



    }
}
