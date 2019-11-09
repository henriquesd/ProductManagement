using Microsoft.EntityFrameworkCore;
using ProductManagement.Business.Interfaces;
using ProductManagement.Business.Models;
using ProductManagement.Data.Context;
using System;
using System.Threading.Tasks;

namespace ProductManagement.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ProductManagementDbContext context) : base(context) { }

        public async Task<Product> GetProductById(Guid id)
        {
            return await Db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
