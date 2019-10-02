using System;
using System.Collections.Generic;
using System.Text;

namespace MYOB.Models
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PayPeriod { get; set; }
        public int AnnualSalary { get; set; }        
        public decimal SuperPercentage { get; set; }
    }
}
