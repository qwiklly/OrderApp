using Microsoft.EntityFrameworkCore;
using TestTask1.Models;

namespace TestTask1.Data
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{
		public DbSet<Order> Orders { get; set; }
	}
}
