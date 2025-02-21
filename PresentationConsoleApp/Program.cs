
using Data.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Data.Interfaces;
using Data.Repositories;
using Business.Interfaces;
using Business.Services;
using PresentationConsoleApp;
using System.Text.Json;
using System.Text.Encodings.Web;

JsonSerializerOptions options = new()
{
    WriteIndented = true,
    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping  
};

var services = new ServiceCollection()
    .AddDbContext<DataContext>(x => x.UseSqlServer(@"Server=localhost,1433;Database=ProjectDB;User Id=sa;Password=CodeGuruOzzy2025!?;TrustServerCertificate=True"))
    .AddScoped<ICustomerRepository, CustomerRepository>()
    .AddScoped<IProjectRepository, ProjectRepository>()
    .AddScoped<IProjectService, ProjectService>()
    .AddScoped<ICustomerService, CustomerService>()
    .AddScoped<MenuDialogs>()
    .BuildServiceProvider();


var menuDialogs = services.GetRequiredService<MenuDialogs>();


var projectService = services.GetRequiredService<IProjectService>();
var customerService = services.GetRequiredService<ICustomerService>();


var customers = await customerService.GetAllCustomersAsync();
Console.WriteLine("Customers on the database");
Console.WriteLine("--------------------------------");
foreach (var customer in customers)
{
    var json = JsonSerializer.Serialize(customer, options);
    Console.WriteLine(json);
}
Console.WriteLine();
Console.WriteLine("Projects on the database");
Console.WriteLine("--------------------------------");
var projects = await projectService.GetAllAsync();
foreach (var project in projects)
{
    var json = JsonSerializer.Serialize(project, options);
    Console.WriteLine(json);
}
Console.WriteLine();
await menuDialogs.MenuOptions();

Console.ReadLine();