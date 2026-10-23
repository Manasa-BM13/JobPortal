namespace EmployeeTaskManagementAPI.Models;

public class JobApplication
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public string ApplicantName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string ResumeSummary { get; set; } = "";
    public string PortfolioUrl { get; set; } = "";
    public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Submitted";

    public JobPost? Job { get; set; }
}
