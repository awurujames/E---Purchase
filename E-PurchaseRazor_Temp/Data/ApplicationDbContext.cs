//using E_PurchaseRazor_Temp.Models;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;

//namespace E_PurchaseRazor_Temp.Data
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
//        {
//        }

//        public DbSet<Category> Categories { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            modelBuilder.Entity<Category>().HasData(
//                new Category { Id = Guid.Parse("B5D495FC-EAB4-4276-B5BE-F599AD4830E6"), DisplayOrder = 2, Name = "test chilling"},
//                new Category { Id = Guid.Parse("0FEBEF14-011B-40DA-A135-D3A09816248B"), DisplayOrder = 2, Name = "test chilling"},
//                new Category { Id = Guid.Parse("0FEBEF14-011B-40DA-A135-D3A09816249B"), DisplayOrder = 2, Name = "test chilling" }
//                ); 
//        }

//    }
//}
