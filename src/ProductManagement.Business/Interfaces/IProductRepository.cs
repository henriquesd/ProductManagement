using ProductManagement.Business.Models;
using System;
using System.Threading.Tasks;

namespace ProductManagement.Business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductById(Guid id);
    }
}
