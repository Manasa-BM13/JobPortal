using EmployeeTaskManagementAPI.Data;
using EmployeeTaskManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTaskManagementAPI.Controllers;

[ApiController]
[Route("api/applications")]
public class ApplicationsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ApplicationsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobApplication>>> GetAll()
        => await _db.JobApplications
            .Include(a => a.Job)
            .OrderByDescending(a => a.AppliedAt)
            .ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<JobApplication>> Get(int id)
    {
        var application = await _db.JobApplications
            .Include(a => a.Job)
            .FirstOrDefaultAsync(a => a.Id == id);

        return application is null ? NotFound() : Ok(application);
    }

    [HttpPost]
    public async Task<ActionResult<JobApplication>> Create(JobApplication application)
    {
        var jobExists = await _db.Jobs.AnyAsync(j => j.Id == application.JobId);
        if (!jobExists)
        {
            return BadRequest("JobId does not exist.");
        }

        application.AppliedAt = DateTime.UtcNow;
        application.Status = "Submitted";
        _db.JobApplications.Add(application);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = application.Id }, application);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromQuery] string status)
    {
        var application = await _db.JobApplications.FindAsync(id);
        if (application is null) return NotFound();

        application.Status = status;
        await _db.SaveChangesAsync();
        return Ok(application);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var application = await _db.JobApplications.FindAsync(id);
        if (application is null) return NotFound();

        _db.JobApplications.Remove(application);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
