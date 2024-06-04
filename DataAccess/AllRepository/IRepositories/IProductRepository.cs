﻿using E_Purchase_DataAccess.Repository.IRepositories;
using E_Purchase_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_jPurchase_DataAccess.AllRepository.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
    }
}