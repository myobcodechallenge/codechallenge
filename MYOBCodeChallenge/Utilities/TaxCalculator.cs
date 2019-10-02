using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MYOBCodeChallenge.Utilities;

namespace MYOBCodeChallenge.Utilities
{
    public static class TaxCalculator
    {
        public static int CalculateIncomeTax(int annualSalary)
        {

            var taxPerMonth = 0;
            if (annualSalary <= Constants.TaxSlab1)
                taxPerMonth = 0;
            else if (annualSalary > Constants.TaxSlab1 && annualSalary <= Constants.TaxSlab2)
                taxPerMonth = ((decimal)(annualSalary - Constants.TaxSlab1) * 0.19m).DivideRoundOffToInt(12);
            else if (annualSalary > Constants.TaxSlab2 && annualSalary <= Constants.TaxSlab3)
                taxPerMonth = (3572m + (decimal)(annualSalary - Constants.TaxSlab2) * 0.325m).DivideRoundOffToInt(12);
            else if (annualSalary > Constants.TaxSlab3 && annualSalary <= Constants.TaxSlab4)
                taxPerMonth = (19822m + (decimal)(annualSalary - Constants.TaxSlab3) * 0.37m).DivideRoundOffToInt(12);
            else if (annualSalary > Constants.TaxSlab4)
                taxPerMonth = (54232m + (decimal)(annualSalary - Constants.TaxSlab4) * 0.45m).DivideRoundOffToInt(12);
            return taxPerMonth;

        }
    }
}
