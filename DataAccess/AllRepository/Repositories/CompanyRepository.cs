using E___Purchase_DataAcces.Data;
using E_Purchase_DataAccess.AllRepository.IRepositories;
using E_Purchase_DataAccess.Repository.Repository;
using E_Purchase_Models;
using E_Purchase_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Purchase_DataAccess.AllRepository.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}
