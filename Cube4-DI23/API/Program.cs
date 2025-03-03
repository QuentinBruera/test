using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Model.Context;
using Model.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();
builder.Services.AddAuthorizationBuilder();

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<DirectoryDbContext>()
    .AddApiEndpoints();

// Add services to the container.
builder.Services.AddDbContext<DirectoryDbContext>();

// Repositories
builder.Services.AddScoped<SiteRepository, SiteRepository>();
builder.Services.AddScoped<EmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<DepartmentRepository, DepartmentRepository>();

// Services
builder.Services.AddScoped<SiteService, SiteService>();
builder.Services.AddScoped<EmployeeService, EmployeeService>();
builder.Services.AddScoped<DepartmentService, DepartmentService>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddOpenApi();

var app = builder.Build();

app.MapIdentityApi<User>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
