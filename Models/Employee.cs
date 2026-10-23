namespace EmployeeTaskManagementAPI.Models;

public class JobPost
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Company { get; set; } = "";
    public string Location { get; set; } = "";
    public string Type { get; set; } = "Full-time";
    public string Category { get; set; } = "";
    public string Description { get; set; } = "";
    public string Requirements { get; set; } = "";
    public decimal Salary { get; set; }
    public DateTime PostedAt { get; set; } = DateTime.UtcNow;
}
