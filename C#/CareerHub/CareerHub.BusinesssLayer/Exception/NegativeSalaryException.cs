using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.BusinesssLayer.Exception
{
    public class NegativeSalaryException : Exception
    {
        public NegativeSalaryException(string message) : base(message) { }
    }
}
