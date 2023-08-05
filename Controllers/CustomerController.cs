using CustomerInvoiceAPI.Data.Entities;
using CustomerInvoiceAPI.Models.Customer;
using CustomerInvoiceAPI.Services.Customer;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace CustomerInvoiceAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerService _customerService;
		private readonly ILogger<CustomerController> _logger;

		public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
		{
			_customerService = customerService;
			_logger = logger;
		}
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CustomerCreate model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					CustomerEntity customer = await _customerService.CreateCustomerAsync(model);

					if (customer != null)
					{
						_logger.LogInformation("Customer with ID: {Id} created successfully", customer.Id);
						return CreatedAtAction(nameof(Get), new { customerId = customer.Id }, customer);
					}
					else
					{
						_logger.LogError("Error occurred while creating customer.");
						return BadRequest("An error occurred while (here) trying to create the customer.");
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error occurred while creating customer.");
					return BadRequest("An error occurred while trying to create the customer.");
				}
			}
			else
			{
				_logger.LogWarning("Invalid model state for the creation of a new customer");
				return BadRequest(ModelState);
			}
		}
		[HttpGet("{customerId}")]
		public async Task<IActionResult> Get([FromRoute] int customerId)
		{
			try
			{
				CustomerEntity customerExists = await _customerService.GetCustomerAsync(customerId);
				if (customerExists != null)
				{

					_logger.LogInformation("Customer with ID: {Id} found.", customerExists.Id);
					return Ok(customerExists);
				}
				else
				{
					_logger.LogWarning("Customer with ID: {Id} not found.", customerId);
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving customer.");
				return BadRequest("An error occurred while trying to retrieve the customer.");
			}
		}
		[HttpDelete("{customerId}")]
		public async Task<IActionResult> Delete([FromRoute] int customerId)
		{
			try
			{
				CustomerEntity customerExists = await _customerService.RemoveCustomerAsync(customerId);
				if (customerExists != null)
				{
					_logger.LogInformation("Customer with ID: {Id} deleted.", customerExists.Id);
					return Ok(customerExists);

				}
				else
				{
					_logger.LogWarning("Customer with ID: {Id} not found.", customerId);
					return NotFound();
				}

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving customer.");
				return BadRequest("An error occurred while trying to retrieve the customer.");
			}
		}
		[HttpPut("{customerId}")]
		public async Task<IActionResult> Update([FromRoute] int customerId, [FromBody] CustomerUpdate model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					CustomerEntity customerExists = await _customerService.GetCustomerAsync(customerId);
					if (customerExists != null)
					{
						await _customerService.UpdateCustomerAsync(customerId, model);
						_logger.LogInformation("Customer with ID: {Id} updated.", customerExists.Id);
						return Ok(customerExists);

					}
					else
					{
						_logger.LogWarning("Customer with ID: {Id} not found.", customerId);
						return NotFound();

					}


				}
				else
				{
					_logger.LogWarning("Invalid model state for the creation of a new customer");
					return BadRequest(ModelState);

				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving customer.");
				return BadRequest("An error occurred while trying to retrieve the customer.");
			}
		}
		[HttpGet("list/all")]
		public async Task<IActionResult> GetAllCustomersAsync()
		{
			try
			{
				IEnumerable<CustomerList> details = await _customerService.GetAllCustomersAsync();
				if (details != null)
				{
					return Ok(details);
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving customers.");
				return BadRequest("An error occurred while trying to retrieve the customers.");
			}
		}
		[HttpGet("list/active")]
		public async Task<IActionResult> GetActiveCustomersAsync()
		{
			try
			{
				IEnumerable<CustomerList> details = await _customerService.GetActiveCustomersAsync();
				if (details != null)
				{
					return Ok(details);
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving customers.");
				return BadRequest("An error occurred while trying to retrieve the customers.");
			}
		}

		[HttpGet("list/deleted")]
		public async Task<IActionResult> GetDeletedCustomersAsync()
		{
			try
			{
				IEnumerable<CustomerList> details = await _customerService.GetDeletedCustomersAsync();
				if (details != null)
				{
					return Ok(details);
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving customers.");
				return BadRequest("An error occurred while trying to retrieve the customers.");
			}
		}
	}
}
