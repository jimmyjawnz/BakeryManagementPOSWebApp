using BakeryManagementPOSWebApp.Data.Enities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata;

namespace BakeryManagementPOSWebApp.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<Employee>(options)
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .Property(p => p.Created)
            .HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Employee>()
            .Property(e => e.Created)
            .HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Customer>()
            .Property(c => c.Created)
            .HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Order>()
            .Property(o => o.Created)
            .HasDefaultValueSql("getdate()");
        modelBuilder.Entity<OrderItem>()
            .Property(oi=> oi.Created)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Employee>()
            .Property(e => e.FullName)
            .HasComputedColumnSql("[employee_first_name] + ' ' + [employee_last_name]");

        modelBuilder.Entity<Customer>()
            .Property(c => c.FullName)
            .HasComputedColumnSql("[customer_first_name] + ' ' + [customer_last_name]");

        modelBuilder.Entity<Customer>()
            .Property(c => c.PhoneAndFullName)
            .HasComputedColumnSql("[customer_phone_number] + ' (' + [customer_first_name] + ' ' + [customer_last_name] + ')'");

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "1e348a66-f22c-48fa-9494-e422b79eea62",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "9636145a-2633-42e3-ada3-b517edced536",
                Name = "Manager",
                NormalizedName = "MANAGER"
            },
            new IdentityRole
            {
                Id = "cd04e65e-405b-445c-8cb4-24b5f6824717",
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                Id = 1,
                FirstName = "ADMIN",
                LastName = string.Empty,
                Created = DateTime.Parse("0001-01-01"),
                PhoneNumber = "0"
            }
        );

        modelBuilder.Entity<Employee>().HasData(
            new Employee
            {
                Id = "e467e64b-a141-4325-b57b-d267cfd6ccf5",
                FirstName = "Admin",
                LastName = "",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAENAdq/abuUcgZqOn4SAT/IDK01N3WDWnQOMJAB+aEccSNjPJgeDWB4E07bVhWPGovw==",
                UserName = "ADMIN",
                NormalizedUserName = "ADMIN",
                SecurityStamp = "c6f3a8ff-a483-4466-b2c6-32313089d489",
                ConcurrencyStamp = "98646101-9f66-4aea-ad13-5250b5c1ddde",
                Created = DateTime.Parse("0001-01-01"),
                PhoneNumber = "0000000000",
                CustomerId = 1
            }
        );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = "e467e64b-a141-4325-b57b-d267cfd6ccf5",
                RoleId = "1e348a66-f22c-48fa-9494-e422b79eea62"
            }
        );
    }
}
