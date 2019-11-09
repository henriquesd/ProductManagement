using ProductManagement.Business.Interfaces;
using ProductManagement.Business.Models;
using System;
using System.Threading.Tasks;

namespace ProductManagement.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Add(Product product)
        {
            product.RegisterDate = DateTime.Now;

            await _productRepository.Add(product);
        }

        public async Task Update(Product product)
        {
            await _productRepository.Update(product);
        }

        public async Task Remove(Guid id)
        {
            await _productRepository.Remove(id);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
