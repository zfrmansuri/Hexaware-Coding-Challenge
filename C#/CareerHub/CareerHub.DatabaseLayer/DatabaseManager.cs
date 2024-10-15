using CareerHub.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.DatabaseLayer
{
    public class DatabaseManager
    {
        // Method to initialize the database schema and tables
        public void InitializeDatabase()
        {
            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();

                //using (SqlCommand cmd = new SqlCommand(query, conn))
                //{
                //    cmd.ExecuteNonQuery();
                //}

            }
        }

        public void ExecuteUserChoice()
        {
            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Retrieve all job listings");
                Console.WriteLine("2. Create applicant profile");
                Console.WriteLine("3. Submit job application");
                Console.WriteLine("4. Post a job listing");
                Console.WriteLine("5. Query job listings by salary range");
                Console.WriteLine("6. Exit");

                Console.Write("Enter your choice (1-6): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Retrieve all job listings
                        var jobListings = GetAllJobListings();
                        Console.WriteLine("Job Title\t\tCompany Name\t\tSalary");
                        Console.WriteLine("-------------------------------------------------------");
                        foreach (var job in jobListings)
                        {
                            Console.WriteLine($"{job.JobTitle}\t\t{job.CompanyName}\t\t{job.Salary:C}");
                        }
                        break;

                    case "2":
                        // Create applicant profile
                        Console.WriteLine("Enter Applicant Details:");
                        Console.Write("Applicant ID: ");
                        int applicantID = int.Parse(Console.ReadLine());
                        Console.Write("First Name: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Last Name: ");
                        string lastName = Console.ReadLine();
                        Console.Write("Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Phone: ");
                        string phone = Console.ReadLine();
                        Console.Write("Resume: ");
                        string resume = Console.ReadLine();

                        Applicant newApplicant = new Applicant(applicantID, firstName, lastName, email, phone, resume);
                        InsertApplicant(newApplicant);
                        break;

                    case "3":
                        // Submit job application
                        Console.WriteLine("Enter Job Application Details:");
                        Console.Write("Application ID: ");
                        int applicationID = int.Parse(Console.ReadLine());
                        Console.Write("Job ID: ");
                        int jobID = int.Parse(Console.ReadLine());
                        Console.Write("Applicant ID: ");
                        int appID = int.Parse(Console.ReadLine());
                        Console.Write("Cover Letter: ");
                        string coverLetter = Console.ReadLine();

                        JobApplication newApplication = new JobApplication(applicationID, jobID, appID, DateTime.Now, coverLetter);
                        InsertJobApplication(newApplication);
                        break;

                    case "4":
                        // Post a job listing
                        Console.WriteLine("Enter Job Details:");
                        Console.Write("Job ID: ");
                        int newJobID = int.Parse(Console.ReadLine());
                        Console.Write("Company ID: ");
                        int companyID = int.Parse(Console.ReadLine());
                        Console.Write("Job Title: ");
                        string jobTitle = Console.ReadLine();
                        Console.Write("Job Description: ");
                        string jobDescription = Console.ReadLine();
                        Console.Write("Job Location: ");
                        string jobLocation = Console.ReadLine();
                        Console.Write("Salary: ");
                        decimal salary = decimal.Parse(Console.ReadLine());
                        Console.Write("Job Type: ");
                        string jobType = Console.ReadLine();

                        JobListing newJob = new JobListing(newJobID, companyID, jobTitle, jobDescription, jobLocation, salary, jobType, DateTime.Now);
                        InsertJobListing(newJob);
                        break;

                    case "5":
                        // Query job listings by salary range
                        Console.WriteLine("Enter Salary Range:");
                        Console.Write("Minimum Salary: ");
                        decimal minSalary = decimal.Parse(Console.ReadLine());
                        Console.Write("Maximum Salary: ");
                        decimal maxSalary = decimal.Parse(Console.ReadLine());

                        var salaryJobListings = GetJobListingsInSalaryRange(minSalary, maxSalary);
                        Console.WriteLine("Job Title\t\tCompany Name\t\tSalary");
                        Console.WriteLine("-------------------------------------------------------");
                        foreach (var job in salaryJobListings)
                        {
                            Console.WriteLine($"{job.JobTitle}\t\t{job.CompanyName}\t\t{job.Salary:C}");
                        }
                        break;

                    case "6":
                        // Exit the application
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                        break;
                }

                Console.WriteLine(); // Add an empty line for better readability
            }
           
        }

        public void InsertJobListing(JobListing job)
        {
            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();
                string query = @"INSERT INTO Jobs (JobID, CompanyID, JobTitle, JobDescription, JobLocation, Salary, JobType, PostedDate)
                                 VALUES (@JobID, @CompanyID, @JobTitle, @JobDescription, @JobLocation, @Salary, @JobType, @PostedDate)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@JobID", job.JobID);
                    cmd.Parameters.AddWithValue("@CompanyID", job.CompanyID);
                    cmd.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                    cmd.Parameters.AddWithValue("@JobDescription", job.JobDescription);
                    cmd.Parameters.AddWithValue("@JobLocation", job.JobLocation);
                    cmd.Parameters.AddWithValue("@Salary", job.Salary);
                    cmd.Parameters.AddWithValue("@JobType", job.JobType);
                    cmd.Parameters.AddWithValue("@PostedDate", job.PostedDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<(string JobTitle, string CompanyName, decimal Salary)> GetAllJobListings()
        {
            List<(string JobTitle, string CompanyName, decimal Salary)> jobListings = new List<(string JobTitle, string CompanyName, decimal Salary)>();

            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();
                string query = @"
            SELECT j.JobTitle, c.CompanyName, j.Salary
            FROM Jobs j
            JOIN Companies c ON j.CompanyID = c.CompanyID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string jobTitle = reader.GetString(0);
                            string companyName = reader.GetString(1);
                            decimal salary = reader.GetDecimal(2);
                            jobListings.Add((jobTitle, companyName, salary));
                        }
                    }
                }
            }

            return jobListings;
        }

        public void InsertApplicant(Applicant applicant)
        {
            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();
                string query = @"INSERT INTO Applicants (ApplicantID, FirstName, LastName, Email, Phone, Resume)
                         VALUES (@ApplicantID, @FirstName, @LastName, @Email, @Phone, @Resume)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicantID", applicant.ApplicantID);
                    cmd.Parameters.AddWithValue("@FirstName", applicant.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", applicant.LastName);
                    cmd.Parameters.AddWithValue("@Email", applicant.Email);
                    cmd.Parameters.AddWithValue("@Phone", applicant.Phone);
                    cmd.Parameters.AddWithValue("@Resume", applicant.Resume);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Applicant profile created successfully.");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("An error occurred while inserting the applicant: " + ex.Message);
                    }
                }
            }
        }

        public void InsertJobApplication(JobApplication application)
        {
            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();
                string query = @"INSERT INTO Applications (ApplicationID, JobID, ApplicantID, ApplicationDate, CoverLetter)
                         VALUES (@ApplicationID, @JobID, @ApplicantID, @ApplicationDate, @CoverLetter)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicationID", application.ApplicationID);
                    cmd.Parameters.AddWithValue("@JobID", application.JobID);
                    cmd.Parameters.AddWithValue("@ApplicantID", application.ApplicantID);
                    cmd.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate);
                    cmd.Parameters.AddWithValue("@CoverLetter", application.CoverLetter);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Job application submitted successfully.");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("An error occurred while submitting the application: " + ex.Message);
                    }
                }
            }
        }

        public List<(string JobTitle, string CompanyName, decimal Salary)> GetJobListingsInSalaryRange(decimal minSalary, decimal maxSalary)
        {
            List<(string JobTitle, string CompanyName, decimal Salary)> jobListings = new List<(string JobTitle, string CompanyName, decimal Salary)>();

            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();
                string query = @"
            SELECT j.JobTitle, c.CompanyName, j.Salary
            FROM Jobs j
            JOIN Companies c ON j.CompanyID = c.CompanyID
            WHERE j.Salary BETWEEN @MinSalary AND @MaxSalary";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MinSalary", minSalary);
                    cmd.Parameters.AddWithValue("@MaxSalary", maxSalary);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string jobTitle = reader.GetString(0);
                            string companyName = reader.GetString(1);
                            decimal salary = reader.GetDecimal(2);
                            jobListings.Add((jobTitle, companyName, salary));
                        }
                    }
                }
            }

            return jobListings;
        }

    }


    
}