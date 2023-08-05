using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomerInvoiceAPI.Data.Entities
{
	public class InvoiceEntity
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime InvoiceDate { get; set; }

		[Required]
		[ForeignKey("Customer")]
		public int CustomerId { get; set; }  // Foreign key to Customer
		[JsonIgnore]
		public virtual CustomerEntity? Customer { get; set; }

		[DataType(DataType.Currency)]
		public decimal TotalAmount { get; set; }
		[Required]
		public bool IsDeleted { get; set; } = false;


		// Navigation Property
		public virtual List<InvoiceLineItemEntity> InvoiceLineItems { get; set; }
	}
}
