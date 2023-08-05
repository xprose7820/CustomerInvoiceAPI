using System.ComponentModel.DataAnnotations;

namespace CustomerInvoiceAPI.Models.Customer
{
	public class CustomerUpdate
	{
		[StringLength(100)]
		public string FirstName { get; set; }
		[StringLength(100)]
		public string LastName { get; set; }
		[EmailAddress]
		public string Email { get; set;}
		[Phone]
		public string Phone { get; set; }

	}
}
