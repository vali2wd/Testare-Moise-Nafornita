using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testare_Moise_Nafornita
{
    internal class Test_Convert
    {
        static void SomeSum()
        {
            int x = 5;
            int y = 10;
            int s = 0;
            for (int i = x; i <= y; i++)
                if (i <= 8)
                    s = s + i;
            Console.WriteLine(s);
        }
    }
}
