using CareerHub.Business.Repository;
using CareerHub.Entity;
using System;
using System.Collections.Generic;

namespace LayeredArchitectureProject.Business.Service
{
    public class JobListingService : IJobListingRepository
    {
        // Internal storage for job listings, applications, and applicants
        private List<JobListing> jobListings = new List<JobListing>();
        private Dictionary<int, List<Applicant>> jobApplications = new Dictionary<int, List<Applicant>>();
        private Dictionary<int, Applicant> applicantProfiles = new Dictionary<int, Applicant>();
        private List<JobApplication> jobApplicationRecords = new List<JobApplication>(); // Store job applications

        // Method to create a profile for an applicant
        public void CreateApplicantProfile(int applicantID, string firstName, string lastName, string email, string phone, string resume)
        {
            if (!applicantProfiles.ContainsKey(applicantID))
            {
                Applicant newApplicant = new Applicant(applicantID, firstName, lastName, email, phone, resume);
                applicantProfiles[applicantID] = newApplicant;
            }
        }

        // Method for an applicant to apply for a specific job
        public void ApplyForJob(int applicantID, int jobID, string coverLetter)
        {
            if (applicantProfiles.ContainsKey(applicantID))
            {
                int newApplicationID = jobApplicationRecords.Count + 1; // Assuming application IDs are auto-incremented
                JobApplication newApplication = new JobApplication(newApplicationID, jobID, applicantID, DateTime.Now, coverLetter);

                // Add the new job application to the jobApplicationRecords list
                jobApplicationRecords.Add(newApplication);

                // Add the applicant to the list of applicants for the job
                if (!jobApplications.ContainsKey(jobID))
                {
                    jobApplications[jobID] = new List<Applicant>();
                }

                jobApplications[jobID].Add(applicantProfiles[applicantID]);
            }
        }

        // Method to get the list of job applications for a specific applicant
        public List<JobApplication> GetJobApplicationsByApplicant(int applicantID)
        {
            return jobApplicationRecords.FindAll(application => application.ApplicantID == applicantID);
        }

        // Existing methods for posting and managing jobs
        public void Apply(int jobID, int applicantID, string coverLetter)
        {
            // Placeholder method
        }

        public List<Applicant> GetApplicants(int jobID)
        {
            if (jobApplications.ContainsKey(jobID))
            {
                return jobApplications[jobID];
            }

            return new List<Applicant>();
        }

        public void PostJob(int companyID, string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType)
        {
            int newJobID = jobListings.Count + 1; // Assuming job IDs are auto-incremented

            JobListing newJob = new JobListing(
                newJobID,
                companyID,
                jobTitle,
                jobDescription,
                jobLocation,
                salary,
                jobType,
                DateTime.Now
            );

            jobListings.Add(newJob);
        }

        public List<JobListing> GetJobsByCompany(int companyID)
        {
            return jobListings.FindAll(job => job.CompanyID == companyID);
        }
    }
}
