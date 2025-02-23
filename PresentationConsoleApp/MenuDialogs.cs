
using Business.Factories;
using Business.Interfaces;

namespace PresentationConsoleApp;

public class MenuDialogs(ICustomerService customerService, IProjectService projectService)
{
    
    private readonly ICustomerService _customerService = customerService;
    private readonly IProjectService _projectService = projectService;


    public async Task MenuOptions()
    {

        Console.WriteLine(" -------- Welcome to the menu dialog! ---------");
        Console.WriteLine("What would you like to do?");

        while (true)
        {
            Console.WriteLine("1. Create a new customer");
            Console.WriteLine("2. View all customers");
            Console.WriteLine("3. Update a customer");
            Console.WriteLine("4. Delete a customer");
            Console.WriteLine("5. Create a new project");
            Console.WriteLine("6. View all projects");
            Console.WriteLine("7. Update a project");
            Console.WriteLine("8. Delete a project");
            Console.WriteLine("9. Exit");
            Console.Write("Select an option (1-9): ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await CreateCustomer();
                    break;
                case "2":
                    await GetAllCustomers();
                    break;
                case "3":
                    await UpdateCustomer();
                    break;
                case "4":
                    await DeleteCustomer();
                    break;
                case "5":
                    await CreateProject();
                    break;
                case "6":
                    await GetAllProjects();
                    break;
                case "7":
                    await UpdateProject();
                    break;
                case "8":
                    await DeleteProject();
                    break;
                    case "9":
                    Console.WriteLine("Exiting the program...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    
                    break;
            } 
        }
        
    }

    private async Task CreateCustomer()
    {
        Console.Write("Enter customer name: ");
        var customerName = Console.ReadLine();

        var form = CustomerFactory.Create();
        
        form.CustomerName = customerName!;
        var customer = await _customerService.CreateCustomerAsync(form);

        if (customer!= null)
        {
            
            Console.WriteLine($"Customer {customer.CustomerName} created successfully.");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Failed to create customer.");
        }
    }

    private async Task GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        Console.WriteLine(" ***** Customers on the database *****");
        Console.WriteLine("------------------------------------------");

        foreach (var customer in customers)
        {
            Console.WriteLine($"Customer Name: {customer?.CustomerName}");
        }
        Console.WriteLine("------------------------------------------");
        Console.WriteLine();
    }
    private async Task UpdateCustomer()
    {
        if (_customerService == null)
    {
        Console.WriteLine("Customer service is not available.");
        return;
    }
        // show the existing customer with ids
        var customers = await _customerService.GetAllCustomersAsync();
        foreach (var cust in customers)
        {
            Console.WriteLine($"ID: {cust?.Id}, Customer Name: {cust?.CustomerName}");
        }

        Console.Write("Enter the customer ID to update: ");
        var customerId = int.Parse(Console.ReadLine()!);

        var customer = await _customerService.GetCustomerAsync(c => c.Id == customerId);
        if (customer != null)
        {
            Console.Write("Enter new customer name: ");
            customer.CustomerName = Console.ReadLine()!;
            
            var updatedCustomer = await _customerService.UpdateCustomerAsync(CustomerFactory.Update(customer));
            Console.WriteLine($"Customer {updatedCustomer.CustomerName} updated successfully.");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Customer not found.");
        }
    }
    private async Task DeleteCustomer()
    {
        if (_customerService == null)
        {
            Console.WriteLine("Customer service is not available.");
            return; // Exit the method if the service is not available  

        }
        Console.Write("Enter the customer ID to delete: ");
        var customerId = int.Parse(Console.ReadLine()!);

        var customer = await _customerService.GetCustomerAsync(c => c.Id == customerId);
        if (customer != null)
        {
            await _customerService.DeleteCustomerAsync(customer.Id);
            Console.WriteLine($"Customer {customer.CustomerName} deleted successfully.");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Customer not found.");
        }
    }

    private async Task CreateProject()
    {
        Console.Write("Enter project title: ");
        var projectTitle = Console.ReadLine();
        Console.Write("Enter project description: ");
        var projectDescription = Console.ReadLine();
        // ask the user to select a customer and assign it to the project
        var customers = await _customerService.GetAllCustomersAsync();
        Console.WriteLine("Select a customer:");
        foreach (var cust in customers)
        {
            Console.WriteLine($"{cust?.Id} - {cust?.CustomerName}");
        }
        Console.Write("Enter the customer ID: ");
        var customerId = int.Parse(Console.ReadLine()!);
        var customer = await _customerService.GetCustomerAsync(c => c.Id == customerId);
        var form = ProjectFactory.Create();
        form.Title = projectTitle!;
        form.Description = projectDescription!;
        form.CustomerId = customer.Id!;
        var project = await _projectService.CreateProjectAsync(form);
        if (project != null)
        {
            Console.WriteLine($"Project {project.Title} created successfully.");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Failed to create project.");
        }
        
    }
    private async Task DeleteProject()
    {
        if (_projectService == null)
        {
            Console.WriteLine("Project service is not available.");
            return; // Exit the method if the service is not available  
        }
        Console.Write("Enter the project ID to delete: ");
        var projectId = int.Parse(Console.ReadLine()!);

        var project = await _projectService.GetProjectAsync(p => p.Id == projectId);
        if (project != null)
        {
            await _projectService.DeleteProjectAsync(project.Id);
            Console.WriteLine($"Project {project.Title} deleted successfully.");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Project not found.");
        }
    }

    private async Task UpdateProject()
    {
        if (_projectService == null)
        {
            Console.WriteLine("Project service is not available.");
            return; // Exit the method if the service is not available  
        }
        Console.Write("Enter the project ID to update: ");
        var projectId = int.Parse(Console.ReadLine()!);

        var project = await _projectService.GetProjectAsync(p => p.Id == projectId);
        if (project != null)
        {
            Console.Write("Enter new project title: ");
            project.Title = Console.ReadLine()!;
            Console.Write("Enter new project description: ");
            project.Description = Console.ReadLine()!;
            var updatedProject = await _projectService.UpdateProjectAsync(ProjectFactory.Update(project));
            Console.WriteLine($"Project {updatedProject.Title} updated successfully.");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Project not found.");
        }
    }

    private async Task GetAllProjects()
    {
        if (_projectService == null)
        {
            Console.WriteLine("Project service is not available.");
        }
        else
        {
            var projects = await _projectService.GetAllAsync();
            Console.WriteLine(" ***** Projects on the database *****");
            Console.WriteLine("------------------------------------------");
            foreach (var project in projects)
            {
                Console.WriteLine($"Project Title: {project?.Title}");
            }
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
    }
    }
}
