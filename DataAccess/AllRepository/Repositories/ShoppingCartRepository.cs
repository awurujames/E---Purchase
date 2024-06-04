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
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        //public void Save()
        //{
        //    _db.SaveChanges();
        //}

        public void Update(ShoppingCart obj)
        {
            _db.ShoppingCarts.Update(obj);
        }

    }
}
