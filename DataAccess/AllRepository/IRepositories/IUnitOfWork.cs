using E___Purchase_DataAcces.Data;
using E_jPurchase_DataAccess.AllRepository.IRepositories;
using E_Purchase_DataAccess.Repository.IRepositories;
using E_Purchase_DataAccess.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Purchase_DataAccess.AllRepository.IRepositories
{
    public interface IUnitOfWork 
    {
         ICategoryRepository Category { get;  }
         IProductRepository Product { get; }
         ICompanyRepository Company { get; }
         IShoppingCartRepository ShoppingCart { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }
         void Save();
    }
}
