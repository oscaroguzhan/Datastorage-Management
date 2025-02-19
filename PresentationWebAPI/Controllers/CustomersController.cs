using Business.Dtos.Customer;
using Business.Interfaces;
using Business.Models;

using Microsoft.AspNetCore.Mvc;

namespace PresentationWebAPI.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController(ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService _customerService = customerService;

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
    }
}
