using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}

		
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Composer> Composers { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Song> Songs { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			
			modelBuilder.Entity<Customer>().ToTable("Customer");
			modelBuilder.Entity<Composer>().ToTable("Composer");
			modelBuilder.Entity<Payment>().ToTable("Payment");
			modelBuilder.Entity<Category>().ToTable("Category");
			modelBuilder.Entity<Song>().ToTable("Song");

		}
	}
}
