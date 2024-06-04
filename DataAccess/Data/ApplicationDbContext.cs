using E_Purchase_Models;
using E_Purchase_Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E___Purchase_DataAcces.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)//82671 82671
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = Guid.Parse("FBFE62DE-D4A1-4285-A9FA-CD7B314A4382"), DisplayOrder = 1, Name = "First Category" },
                new Category { Id = Guid.Parse("FBFE62DE-D4A1-4285-A9FA-CD7B314A4356"), DisplayOrder = 2, Name = "Second Category" },
                new Category { Id = Guid.Parse("FBFE62DE-D4A1-4285-A9FA-CD7B314A4398"), DisplayOrder = 3, Name = "Third Category" },
                new Category { Id = Guid.Parse("FBFE62DE-D4A1-4285-A9FA-CD7B314A4394"), DisplayOrder = 4, Name = "Third Category" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = Guid.Parse("FBFE62DE-D4A9-4285-A9FA-CD7B314A4322"), ISBN ="ISBN001" , Title = "First Title1", Brand = "New Brand1", Author = "First Author", Description = "Description1", ListPrice = 100, Price = 200, Price100 = 300, Price50 = 400, CategoryId = Guid.Parse("FBFE62DE-D4A1-4285-A9FA-CD7B314A4382"), ImageUrl = "" },
                new Product { Id = Guid.Parse("FBFE62DE-D4A9-4285-A9FA-CD7B314A4383"), ISBN ="ISBN004" , Title = "First Title4", Brand = "New Bran4", Author = "First Author4", Description = "Description2", ListPrice = 101, Price = 201, Price100 = 301, Price50 = 401, CategoryId = Guid.Parse("FBFE62DE-D4A1-4285-A9FA-CD7B314A4394"), ImageUrl = "" },
                new Product { Id = Guid.Parse("FBFE62DE-D4A9-4285-A9FA-CD7B314A4374"), ISBN ="ISBN003" , Title = "Second Title2", Brand = "New Bran2", Author = "First Author3", Description = "Description3", ListPrice = 102, Price = 202, Price100 = 302, Price50 = 402, CategoryId = Guid.Parse("FBFE62DE-D4A1-4285-A9FA-CD7B314A4356"), ImageUrl = "" },
                new Product { Id = Guid.Parse("FBFE62DE-D4A9-4285-A9FA-CD7B314A4382"), ISBN ="ISBN003" , Title = "Third Title3", Brand = "New Bran3", Author = "First Author4", Description = "Description4", ListPrice = 104, Price = 204, Price100 = 304, Price50 = 404, CategoryId = Guid.Parse("FBFE62DE-D4A1-4285-A9FA-CD7B314A4398"), ImageUrl = "" }
                );

            modelBuilder.Entity<Company>().HasData(
               new Company { Id = Guid.Parse("FBFE62DE-D4A9-4285-A9FA-CD7B314A4322"), City = "Ota", Name = "SDSD Prestige", State = "Ogun", StreetAddress = "First Street Address", PostalCode = "12098", PhoneNumber = "090181817837" },
               new Company { Id = Guid.Parse("FBFE62DB-D4A9-4285-A9FA-CD7B314A4383"), City = "Lekki", Name = "Interswitch ", State = "Lagos", StreetAddress = "Second Street Address", PostalCode = "27654", PhoneNumber = "08018374645" },
               new Company { Id = Guid.Parse("FBFE62DA-D4A9-4285-A9FA-CD7B314A4374"), City = "Suleja", Name = "Ors Hotels ", State = "Abuja", StreetAddress = "Third Street Address", PostalCode = "10293", PhoneNumber = "0801264744" },
               new Company { Id = Guid.Parse("FBFE62DC-D4A9-4285-A9FA-CD7B314A4382"), City = "Auchi", Name = "Interllude", State = "London", StreetAddress = "Fourth Street Address", PostalCode = "38475", PhoneNumber = "090284748" }
               );
        }

    }
}