using CareerHub.Entity;
using System.Collections.Generic;

namespace CareerHub.Business.Repository
{
    public interface IJobListingRepository
    {
        // Job application methods
        void Apply(int jobID, int applicantID, string coverLetter);
        List<Applicant> GetApplicants(int jobID);

        // Job posting methods
        void PostJob(int companyID, string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType);
        List<JobListing> GetJobsByCompany(int companyID);

        // Applicant methods
        void CreateApplicantProfile(int applicantID, string firstName, string lastName, string email, string phone, string resume);
        void ApplyForJob(int applicantID, int jobID, string coverLetter);

        // Job application methods
        List<JobApplication> GetJobApplicationsByApplicant(int applicantID);
    }
}
