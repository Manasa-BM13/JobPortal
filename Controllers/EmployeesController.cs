using EmployeeTaskManagementAPI.Data;
using EmployeeTaskManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTaskManagementAPI.Controllers;

[ApiController]
[Route("api/jobs")]
public class JobsController : ControllerBase
{
    private readonly AppDbContext _db;

    public JobsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobPost>>> GetAll()
        => await _db.Jobs.OrderByDescending(j => j.PostedAt).ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<JobPost>> Get(int id)
    {
        var job = await _db.Jobs.FindAsync(id);
        return job is null ? NotFound() : Ok(job);
    }

    [HttpPost]
    public async Task<ActionResult<JobPost>> Create(JobPost job)
    {
        job.PostedAt = DateTime.UtcNow;
        _db.Jobs.Add(job);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = job.Id }, job);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, JobPost input)
    {
        var job = await _db.Jobs.FindAsync(id);
        if (job is null) return NotFound();

        job.Title = input.Title;
        job.Company = input.Company;
        job.Location = input.Location;
        job.Type = input.Type;
        job.Category = input.Category;
        job.Description = input.Description;
        job.Requirements = input.Requirements;
        job.Salary = input.Salary;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var job = await _db.Jobs.FindAsync(id);
        if (job is null) return NotFound();

        _db.Jobs.Remove(job);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
