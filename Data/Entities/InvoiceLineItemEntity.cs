using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomerInvoiceAPI.Data.Entities
{
	public class InvoiceLineItemEntity
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey("Invoice")]
		public int InvoiceId { get; set; }  // Foreign key to Invoice
		[JsonIgnore]
		public virtual InvoiceEntity? Invoice { get; set; }


		[Required]
		[StringLength(100)]
		public string Description { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		[Required]
		public int Quantity { get; set; }

	}
}
