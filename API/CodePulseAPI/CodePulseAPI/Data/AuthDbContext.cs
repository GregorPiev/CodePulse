using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CodePulseAPI.Data
{
	public class AuthDbContext : IdentityDbContext
	{
		public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			var readerRoleId = "717999cc-0324-4df7-939e-a977bc42f339"; 
			var writerRoleId = "ebdf5ea1-a3b0-423d-8c13-5b725a4f4101";



			//Create Reader and Writer Role
			var roles = new List<IdentityRole>
			{
				new()
				{
					Id = readerRoleId,
					Name = "Reader",
					NormalizedName = "Reader".ToUpper(),
					ConcurrencyStamp = readerRoleId
				},
				new()
				{
					Id = writerRoleId,
					Name = "Writer",
					NormalizedName = "Writer".ToUpper(),
					ConcurrencyStamp = writerRoleId
				}
			};

			//Seed the roles
			builder.Entity<IdentityRole>().HasData(roles);

			//Create Admin user
			var adminUserId = "9d79c827-4775-462d-98e5-eb65cd13be1d";
			var admin = new IdentityUser()
			{
				Id = adminUserId,
				UserName = "admin@gregory.com",
				Email = "admin@gregory.com",
				NormalizedEmail = "admin@gregory.com".ToUpper(),
				NormalizedUserName = "admin@gregory.com".ToUpper()				
			};

			admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

			builder.Entity<IdentityUser>().HasData(admin);
			//Give roles to Admin user
			var adminRoles = new List<IdentityUserRole<string>>()
			{
				new()
				{
					UserId = adminUserId,
					RoleId = readerRoleId
				},
				new()
				{
					UserId = adminUserId,
					RoleId = writerRoleId
				}
			};
			builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
		}
	}
}
