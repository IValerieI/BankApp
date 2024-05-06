namespace BankApp
{
    public class Converter
    {
        // R - rubles
        // D - dollars
        // E - euros
        // P - pounds

        // R to D - sum * fromRtoD
        // D to R - sum / fromRtoD
        private decimal fromRtoD = 0.011m;
        private decimal fromRtoE = 0.01m;
        private decimal fromRtoP = 0.0087m;

        private decimal fromDtoE = 0.93m;
        private decimal fromDtoP = 0.8m;

        private decimal fromEtoP = 0.86m;

        public decimal ConvertRublesTo(decimal amount, string currency)
        {
            switch (currency)
            {
                case "dollars":
                    return amount * fromRtoD;
                case "euros":
                    return amount * fromRtoE;
                case "pounds":
                    return amount * fromRtoP;
                default:
                    Console.WriteLine("Wrong option. Choose option from list above.");
                    return -1;
            }
        }

        public decimal ConvertDollarsTo(decimal amount, string currency)
        {
            switch (currency)
            {
                case "rubles":
                    return amount / fromRtoD;
                case "euros":
                    return amount * fromDtoE;
                case "pounds":
                    return amount * fromDtoP;
                default:
                    Console.WriteLine("Wrong option. Choose option from list above.");
                    return -1;
            }
        }

        public decimal ConvertEurosTo(decimal amount, string currency)
        {
            switch (currency)
            {
                case "rubles":
                    return amount / fromRtoE;
                case "dollars":
                    return amount / fromDtoE;
                case "pounds":
                    return amount * fromEtoP;
                default:
                    Console.WriteLine("Wrong option. Choose option from list above.");
                    return -1;
            }
        }

        public decimal ConvertPoundsTo(decimal amount, string currency)
        {
            switch (currency)
            {
                case "rubles":
                    return amount / fromRtoP;
                case "dollars":
                    return amount / fromDtoP;
                case "euros":
                    return amount / fromEtoP;
                default:
                    Console.WriteLine("Wrong option. Choose option from list above.");
                    return -1;
            }
        }

    }
}
