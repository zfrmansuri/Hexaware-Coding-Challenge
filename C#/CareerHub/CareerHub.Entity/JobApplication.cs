using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.Entity
{
    public class JobApplication
    {
        // Properties
        public int ApplicationID { get; set; }
        public int JobID { get; set; }
        public int ApplicantID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string CoverLetter { get; set; }

        // Constructor
        public JobApplication(int applicationID, int jobID, int applicantID, DateTime applicationDate, string coverLetter)
        {
            ApplicationID = applicationID;
            JobID = jobID;
            ApplicantID = applicantID;
            ApplicationDate = applicationDate;
            CoverLetter = coverLetter;
        }
    }
}
