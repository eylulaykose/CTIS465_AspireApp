using APP.Projects.Domain;
using APP.Projects.Features.Tags;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// ======================
// Add services
// ======================

// 1) DbContext registration (SQLite)
var connectionString = builder.Configuration.GetConnectionString(nameof(ProjectsDb));
builder.Services.AddDbContext<DbContext, ProjectsDb>(options =>
    options.UseSqlite(connectionString));

// 2) MediatR registration – scan assemblies
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(TagCreateHandler).Assembly));

// 3) Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProjectsDb>();
    db.Database.EnsureCreated();
}

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();