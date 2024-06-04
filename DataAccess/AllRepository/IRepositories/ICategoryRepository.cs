using E___Purchase.Models;
using E_Purchase_DataAccess.Repository.IRepositories;
using E_Purchase_Models;
using E_Purchase_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Purchase_DataAccess.AllRepository.IRepositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
    }
}
