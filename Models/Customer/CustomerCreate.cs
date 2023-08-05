using System.ComponentModel.DataAnnotations;

namespace CustomerInvoiceAPI.Models.Customer
{
	public class CustomerCreate
	{

		[Required]
		[StringLength(100)]
		public string FirstName { get; set; }
		[Required]
		[StringLength(100)]
		public string LastName { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[Phone]
		public string Phone { get; set; }

	
	}
}
