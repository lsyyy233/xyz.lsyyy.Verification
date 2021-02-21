using Microsoft.EntityFrameworkCore;

namespace xyz.lsyyy.Verification.Test.Data
{
	public class MyDbContext : DbContext
	{
		public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
		{
		}
		public DbSet<User> Users { get; set; }
	}
}
