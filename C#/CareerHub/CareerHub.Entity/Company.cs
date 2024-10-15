using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.Entity
{
    public class Company
    {
        // Properties
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }

        // Constructor
        public Company(int companyID, string companyName, string location)
        {
            CompanyID = companyID;
            CompanyName = companyName;
            Location = location;
        }
    }
}
