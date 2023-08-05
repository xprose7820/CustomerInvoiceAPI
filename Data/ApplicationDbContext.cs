using CustomerInvoiceAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace CustomerInvoiceAPI.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<CustomerEntity> Customers { get; set; }
		public DbSet<CustomerAddressEntity> CustomerAddresses { get; set; }
		public DbSet<InvoiceEntity> Invoices { get; set; }
		public DbSet<InvoiceLineItemEntity> InvoicesLineItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// ... other configurations ...

		}
	}
}
