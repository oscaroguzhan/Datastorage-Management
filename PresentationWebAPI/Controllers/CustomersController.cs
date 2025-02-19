using Business.Dtos.Customer;
using Business.Interfaces;
using Business.Models;

using Microsoft.AspNetCore.Mvc;

namespace PresentationWebAPI.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerRegistrationForm form) 
        {
            if(ModelState.IsValid)
            {
                if(await _customerService.CheckIfCustomerExistsAsync(c => c.CustomerName == form.CustomerName))
                {
                    return Conflict("Customer already exists");
                }

                var customer = await _customerService.CreateCustomerAsync(form);
                if(customer!= null)
                {
                    return Ok(customer);
                }
    
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
                
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (ModelState.IsValid)
            {
                var customer = await _customerService.DeleteCustomerAsync(id);
                if (customer)
                {
                    return Ok("Customer deleted");
                }
                else    
                {   
                    return BadRequest();
                }
    
            }
                return BadRequest();
        }

        [HttpGet]       
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return customers.ToList();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var customer = await _customerService.GetCustomerAsync(c => c.Id == id);            
            return Ok(customer);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CustomerUpdateForm form)
        {
            if (ModelState.IsValid)
            {
                var customer = await _customerService.UpdateCustomerAsync(form);
                if (customer != null)
                {
                    return Ok(customer);
                }
                else
                {
                    return NotFound("Customer not found or update failed");
                }
            }
            return BadRequest(ModelState);
        }
    }
}
