using CustomerInvoiceAPI.Data.Entities;
using CustomerInvoiceAPI.Models.Customer;
using CustomerInvoiceAPI.Models.CustomerAddress;
using CustomerInvoiceAPI.Models.Invoice;
using CustomerInvoiceAPI.Services.Customer;
using CustomerInvoiceAPI.Services.Invoice;
using Microsoft.AspNetCore.Mvc;

namespace CustomerInvoiceAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]

	public class InvoiceController : ControllerBase
	{
		private readonly ICustomerService _customerService;
		private readonly IInvoiceService _invoiceService;
		private readonly ILogger<CustomerController> _logger;

		public InvoiceController(ICustomerService customerService, IInvoiceService invoiceService, ILogger<CustomerController> logger)
		{
			_customerService = customerService;
			_invoiceService = invoiceService;
			_logger = logger;
		}
		[HttpPost("{customerId}")]
		public async Task<IActionResult> Create([FromRoute] int customerId, [FromBody] InvoiceCreate model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					CustomerEntity customerExists = await _customerService.GetCustomerAsync(customerId);
					if (customerExists != null)
					{
						InvoiceEntity invoice = await _invoiceService.CreateInvoiceAsync(customerId, model);

						if (invoice != null)
						{
							_logger.LogInformation("Customer with ID: {Id} created successfully", invoice.Id);
							return Ok(invoice);
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
		[HttpGet("{invoiceId}")]
		public async Task<IActionResult> Get([FromRoute] int invoiceId)
		{
			try
			{
				InvoiceEntity invoiceExists = await _invoiceService.GetInvoiceAsync(invoiceId);
				if (invoiceExists != null)
				{
					_logger.LogInformation("Customer Address with ID: {Id} found.", invoiceExists.Id);
					return Ok(invoiceExists);
				}
				else
				{
					_logger.LogWarning("Customer with ID: {Id} not found.", invoiceExists);
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving customer address.");
				return BadRequest("An error occurred while trying to retrieve the customer address.");
			}
		}
		[HttpGet("list/all")]
		public async Task<IActionResult> GetAllInvoices()
		{
			try
			{
				IEnumerable<InvoiceList> details = await _invoiceService.GetAllInvoiceAsync();
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
		[HttpGet("list/{customerId}")]
		public async Task<IActionResult> GetInvoicesByCustomerId([FromRoute] int customerId)
		{
			try
			{
				CustomerEntity customerExists = await _customerService.GetCustomerAsync(customerId);
				if (customerExists != null)
				{
					IEnumerable<InvoiceList> details = await _invoiceService.GetInvoicesByCustomerId(customerId);

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
		[HttpPut("{invoiceId}")]
		public async Task<IActionResult> Update([FromRoute] int invoiceId, InvoiceUpdate model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					InvoiceEntity invoiceExists = await _invoiceService.GetInvoiceAsync(invoiceId);
					if (invoiceExists != null)
					{

						await _invoiceService.UpdateInvoiceAsync(invoiceId, model);
						_logger.LogInformation("Customer with ID: {Id} updated.", invoiceExists.Id);
						return Ok(invoiceExists);

					}
					else
					{
						_logger.LogWarning("Customer with ID: {Id} not found.", invoiceExists);
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
		[HttpDelete("{invoiceId}")]
		public async Task<IActionResult> Delete([FromRoute] int invoiceId)
		{
			try
			{
				InvoiceEntity invoiceExists = await _invoiceService.DeleteInvoiceAsync(invoiceId);
				if (invoiceExists != null)
				{
					_logger.LogInformation("Customer with ID: {Id} deleted.", invoiceExists.Id);
					return Ok(invoiceExists);

				}
				else
				{
					_logger.LogWarning("Customer with ID: {Id} not found.", invoiceId);
					return NotFound();
				}

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving customer.");
				return BadRequest("An error occurred while trying to retrieve the customer.");
			}
		}
	}

}

