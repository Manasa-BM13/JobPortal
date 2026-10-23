using EmployeeTaskManagementAPI.Data;
using EmployeeTaskManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("JobPortalDb"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Jobs.Any())
    {
        db.Jobs.AddRange(
            new JobPost
            {
                Title = "Senior .NET Developer",
                Company = "TechNova Labs",
                Location = "Bengaluru, India",
                Type = "Full-time",
                Category = "Software Development",
                Description = "Build scalable backend services for enterprise products.",
                Requirements = "C#, ASP.NET Core, REST APIs, SQL, teamwork.",
                Salary = 1800000,
                PostedAt = DateTime.UtcNow.AddDays(-2)
            },
            new JobPost
            {
                Title = "UI/UX Designer",
                Company = "PixelCraft",
                Location = "Remote",
                Type = "Remote",
                Category = "Design",
                Description = "Design clean and responsive interfaces for web apps.",
                Requirements = "Figma, design systems, user research, prototyping.",
                Salary = 1200000,
                PostedAt = DateTime.UtcNow.AddDays(-1)
            },
            new JobPost
            {
                Title = "Product Analyst",
                Company = "GrowthHub",
                Location = "Hyderabad, India",
                Type = "Full-time",
                Category = "Business",
                Description = "Translate product data into actionable roadmap decisions.",
                Requirements = "Analytics, SQL, stakeholder communication, Excel.",
                Salary = 1400000,
                PostedAt = DateTime.UtcNow.AddHours(-12)
            }
        );

        db.SaveChanges();
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();
