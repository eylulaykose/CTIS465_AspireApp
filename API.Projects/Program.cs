using APP.Projects.Domain;
using APP.Projects.Features.BookTags;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// DbContext registration (SQLite)
var connectionString = builder.Configuration.GetConnectionString(nameof(ProjectsDb));
builder.Services.AddDbContext<DbContext, ProjectsDb>(options =>
    options.UseSqlite(connectionString));

// MediatR registration
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(BookTagCreateHandler).Assembly));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProjectsDb>();
    db.Database.EnsureCreated(); // DB guaranteed
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