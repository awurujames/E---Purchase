using E___Purchase_DataAcces.Data;
using E_jPurchase_DataAccess.AllRepository.IRepositories;
using E_Purchase_DataAccess.Repository.Repository;
using E_Purchase_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Purchase_DataAccess.AllRepository.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
        public void Update(Product obj)
        {
            _db.Products.Update(obj);

            var objFromDb = _db.Products.FirstOrDefault(x => x.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Author = obj.Author;
                objFromDb.Brand = obj.Brand;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price = obj.Price;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;

                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
