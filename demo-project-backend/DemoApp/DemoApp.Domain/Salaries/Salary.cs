using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Domain.Salaries
{
    public class Salary
    {
        public Guid SalaryID { get; set; }
        public Guid EmployeeID { get; set; }
        public decimal SalaryAmount { get; set; }
        public string Month { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
