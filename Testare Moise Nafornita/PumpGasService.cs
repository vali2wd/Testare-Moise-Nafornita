using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testare_Moise_Nafornita
{
    public class PumpGasService
    {
        private decimal tankCapacity = 50m;

        public PumpGasService(decimal yourCredit, decimal gasPrice)
        {
            YourCredit = yourCredit;
            GasPrice = gasPrice;
        }

        public decimal YourCredit { get; set; }
        
        public decimal GasPrice { get; set; }

        public string PumpGas(string option, int litersToPump = 1, string fuelType = "diesel")
        {
            Console.WriteLine("How much gas would you like to pump?");
            Console.WriteLine("1. Fill tank of all my money!");
            Console.WriteLine("2. Fill tank with a specific amount of gas");

            if (option == "1")
            {
                while(this.YourCredit > 0)
                {
                    // You may go in debt if you are not already.
                    this.tankCapacity -= 1;
                    this.YourCredit -= this.GasPrice;
                    if (this.tankCapacity == 0)
                    {
                        if (fuelType=="diesel" || fuelType == "petrol")
                        {
                            return ("Tank filled up!");
                        }
                        else
                        {
                            return ("Wrong fuel type!");
                        }
                        return ("Tank filled up!");
                    }
                }
                return ("Filled with what was left in your bank account.");
            }
            else if (option == "2")
            {
                Console.WriteLine("How many liters of gas would you like to buy?");
                var amountToPay = litersToPump * this.GasPrice;
                if (amountToPay > this.YourCredit)
                {
                    return("You don't have enough money to buy this amount of gas.");
                }
                else
                {
                    this.YourCredit -= amountToPay;
                    this.tankCapacity -= litersToPump;
                    return("Filled up with specified amount of gas!");
                }
            }
            else
            {
                return("IOE");
            }
        }
    }
}
