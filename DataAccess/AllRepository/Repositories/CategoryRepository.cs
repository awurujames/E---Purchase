using E___Purchase.Models;
using E___Purchase_DataAcces.Data;
using E_Purchase_Models.Models;
using E_Purchase_DataAccess.AllRepository.IRepositories;
using E_Purchase_DataAccess.Repository.IRepositories;
using E_Purchase_DataAccess.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Purchase_Models;

namespace E_Purchase_DataAccess.AllRepository.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        //public void Save()
        //{
        //    _db.SaveChanges();
        //}

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }

    }
}
