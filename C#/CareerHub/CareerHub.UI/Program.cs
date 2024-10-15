using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.DatabaseLayer;
using CareerHub.Entity;

namespace CareerHub.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseManager dbManager = new DatabaseManager();
            dbManager.ExecuteUserChoice();
        }
    }
}
