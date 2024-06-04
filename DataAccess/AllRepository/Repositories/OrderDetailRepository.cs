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
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private ApplicationDbContext _db;
        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       
        public void Update(OrderDetail obj)
        {
            _db.OrderDetails.Update(obj);
        }

    }
}
