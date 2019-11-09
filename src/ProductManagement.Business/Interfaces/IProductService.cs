using ProductManagement.Business.Models;
using System;
using System.Threading.Tasks;

namespace ProductManagement.Business.Interfaces
{
    public interface IProductService : IDisposable
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Remove(Guid id);
    }
}
