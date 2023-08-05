using CustomerInvoiceAPI.Data.Entities;
using CustomerInvoiceAPI.Models.CustomerAddress;
using CustomerInvoiceAPI.Services.Customer;
using CustomerInvoiceAPI.Services.CustomerAddress;
using Microsoft.AspNetCore.Mvc;

namespace CustomerInvoiceAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]

	public class CustomerAddressController : ControllerBase
	{

		private readonly ICustomerService _customerService;
		private readonly ICustomerAddressService _customerAddressService;

		private readonly ILogger<CustomerAddressController> _logger;

		public CustomerAddressController(ICustomerAddressService customerAddressService, ICustomerService customerService, ILogger<CustomerAddressController> logger)
		{
			_customerAddressService = customerAddressService;
			_customerService = customerService;
			_logger = logger;
		}
		[HttpPost("{customerId}")]
		public async Task<IActionResult> Create([FromRoute] int customerId, [FromBody] CustomerAddressCreate model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					CustomerEntity customerExists = await _customerService.GetCustomerAsync(customerId);
					if (customerExists != null)
					{
						CustomerAddressEntity customerAddress = await _customerAddressService.CreateCustomerAddressAsync(customerId, model);

						if (customerAddress != null)
						{
							_logger.LogInformation("Customer with ID: {Id} created successfully", customerAddress.Id);
							return Ok(customerAddress);
						}
						else
						{
							_logger.LogError("Error occurred while creating customer.");
							return BadRequest("An error occurred while trying to create the customer.");
						}

					}
					else
					{
						_logger.LogWarning("Customer with ID: {Id} not found.", customerId);
						return NotFound();

					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error occurred while creating address.");
					return BadRequest("An error occurred while trying to create the address.");

				}
			}
			else
			{
				_logger.LogWarning("Invalid model state for the creation of a new address");
				return BadRequest(ModelState);
			}

		}
		[HttpGet("{customerAddressId}")]
		public async Task<IActionResult> Get([FromRoute] int customerAddressId)
		{
			try
			{
				CustomerAddressEntity customerAddressExists = await _customerAddressService.GetCustomerAddressAsync(customerAddressId);
				if (customerAddressExists != null)
				{
					_logger.LogInformation("Customer Address with ID: {Id} found.", customerAddressExists.Id);
					return Ok(customerAddressExists);
				}
				else
				{
					_logger.LogWarning("Customer with ID: {Id} not found.", customerAddressExists);
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving customer address.");
				return BadRequest("An error occurred while trying to retrieve the customer address.");
			}
		}
		[HttpGet("list/{customerId}")]
		public async Task<IActionResult> GetCustomerAddressesByCustomerId([FromRoute] int customerId)
		{
			try
			{
				CustomerEntity customerExists = await _customerService.GetCustomerAsync(customerId);
				if (customerExists != null)
				{
					IEnumerable<CustomerAddressList> details = await _customerAddressService.GetAddressesByCustomerIdAsync(customerId);

					if (details != null)
					{
						return Ok(details);
					}
					else
					{
						return NotFound();
					}

				}
				else
				{
					_logger.LogWarning("Customer with ID: {Id} not found.", customerId);
					return NotFound();

				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving customers.");
				return BadRequest("An error occurred while trying to retrieve the customers.");

			}
		}
		[HttpPut("{customerAddressId}")]
		public async Task<IActionResult> Update([FromRoute] int customerAddressId, [FromBody] CustomerAddressUpdate model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					CustomerAddressEntity customerAddressExists = await _customerAddressService.GetCustomerAddressAsync(customerAddressId);
					if (customerAddressExists != null)
					{

						await _customerAddressService.UpdateCustomerAddressAsync(customerAddressId, model);
						_logger.LogInformation("Customer with ID: {Id} updated.", customerAddressExists.Id);
						return Ok(customerAddressExists);

					}
					else
					{
						_logger.LogWarning("Customer with ID: {Id} not found.", customerAddressId);
						return NotFound();
					}

				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error occurred while retrieving customer.");
					return BadRequest("An error occurred while trying to retrieve the customer.");
				}
			}
			else
			{
				_logger.LogWarning("Invalid model state for the creation of a new address");
				return BadRequest(ModelState);
			}
		}
		[HttpDelete("{customerAddressId}")]
		public async Task<IActionResult> Delete([FromRoute] int customerAddressId)
		{
			try
			{
				CustomerAddressEntity customerAddressExists = await _customerAddressService.GetCustomerAddressAsync(customerAddressId);
				if (customerAddressExists != null)
				{
					await _customerAddressService.DeleteCustomerAddressAsync(customerAddressId);


					_logger.LogInformation("Customer Address with ID: {Id} found.", customerAddressExists.Id);
					return Ok(customerAddressExists);
				}
				else
				{
					_logger.LogWarning("Customer with ID: {Id} not found.", customerAddressId);
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

