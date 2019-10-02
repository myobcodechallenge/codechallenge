using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYOBCodeChallenge.Utilities
{
    public static class DecimalExtensions
    {
        public static int DivideRoundOffToInt(this decimal value, int divisor)
        {
            return (int)decimal.Round((value / divisor), 0, MidpointRounding.AwayFromZero);
        }

    }
}
