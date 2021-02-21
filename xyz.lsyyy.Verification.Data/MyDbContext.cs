using Microsoft.EntityFrameworkCore;

namespace xyz.lsyyy.Verification.Data
{
	public class MyDbContext : DbContext
	{
		public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
		{
		}
		public DbSet<Action> Actions { get; set; }

		public DbSet<User> Users { get; set; }

		public DbSet<Department> Departments { get; set; }

		public DbSet<Position> Positions { get; set; }

		public DbSet<PositionActionMap> PositionActionMaps { get; set; }

		public DbSet<DepartmentActionMap> DepartmentActionMaps { get; set; }

		public DbSet<UserActionMap> UserActionMaps { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PositionActionMap>().HasNoKey();
			modelBuilder.Entity<PositionActionMap>().HasIndex(x => x.PositionId);
			modelBuilder.Entity<PositionActionMap>().HasIndex(x => x.ActionId);

			modelBuilder.Entity<DepartmentActionMap>().HasNoKey();
			modelBuilder.Entity<DepartmentActionMap>().HasIndex(x => x.DepartmentId);
			modelBuilder.Entity<DepartmentActionMap>().HasIndex(x => x.ActionId);

			modelBuilder.Entity<UserActionMap>().HasNoKey();
			modelBuilder.Entity<UserActionMap>().HasIndex(x => x.UserId);
			modelBuilder.Entity<UserActionMap>().HasIndex(x => x.ActionId);

			base.OnModelCreating(modelBuilder);
		}
	}
}
