using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testare_Moise_Nafornita
{
    public enum Speed
    {
        Slow, Fast
    }
    public enum Distance
    {
        Short, Long
    }
    public enum Car
    {
        Hatchback, SUV
    }
    public class DistanceToCoverService : IDistanceToCoverService
    {
        public string WhereAreYouGoingToday(Distance distance, Speed speed, Car car)
        {
            if (distance == Distance.Short)
            {
                if (speed == Speed.Slow)
                {
                    if (car == Car.Hatchback)
                    {
                        return ("You should walk");
                    }
                    else
                    {
                        return ("You should take the bus");
                    }
                }
                else
                {
                    if (car == Car.Hatchback)
                    {
                        return ("You should drive");
                    }
                    else
                    {
                        return ("You should take a taxi");
                    }
                }
            }
            return string.Empty;
        }
    }
}
