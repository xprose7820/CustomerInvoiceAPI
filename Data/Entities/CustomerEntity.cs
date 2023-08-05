using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerInvoiceAPI.Data.Entities
{
	public class CustomerEntity
	{

		[Key]
		public int Id { get; set; }
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

		[Required]
		public bool IsDeleted { get; set; } = false;
		public virtual List<CustomerAddressEntity> Addresses { get; set; }
		public virtual List<InvoiceEntity> Invoices { get; set; }
	}
}
