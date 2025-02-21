
using Business.Dtos.Customer;
using Business.Factories;
using Business.Interfaces;
using Business.Services;

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
                /*case "2":
                    await _customerService.GetAllCustomers();
                    break;
                case "3":
                    await _customerService.UpdateCustomer();
                    break;
                case "4":
                    await _customerService.DeleteCustomer();
                    break;
                case "5":
                    await _projectService.CreateProject();
                    break;
                case "6":
                    await _projectService.GetAllProjects();
                    break;    
                case "7":   //
                    await _projectService.UpdateProject();
                    break;
                case "8":
                    await _projectService.DeleteProject();
                    break;
                case "9":
                    return;*/
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
}
