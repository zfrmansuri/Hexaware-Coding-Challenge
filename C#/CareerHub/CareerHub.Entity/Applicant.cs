﻿namespace CareerHub.Entity
{
    public class Applicant
    {
        // Properties
        public int ApplicantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Resume { get; set; }

        // Constructor
        public Applicant(int applicantID, string firstName, string lastName, string email, string phone, string resume)
        {
            ApplicantID = applicantID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Resume = resume;
        }
    }
}
