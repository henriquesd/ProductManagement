using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using ProductManagement.Business.Interfaces;
using ProductManagement.App.ViewModels;
using ProductManagement.Business.Models;
using System.Text;
using Newtonsoft.Json;

namespace DevIO.App.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository,
                                    IProductService productService,
                                    IMapper mapper)
        {
            _productRepository = productRepository;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetAll()));
        }

        [Route("new-product")]
        public IActionResult Create()
        {
            return View(new ProductViewModel());
        }

        [HttpPost]
        [Route("new-product")]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return View(productViewModel);

            await _productService.Add(_mapper.Map<Product>(productViewModel));

            return RedirectToAction("Index");
        }

        [Route("product-info/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null) return NotFound();

            return View(productViewModel);
        }

        [Route("edit-product/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null) return NotFound();

            return View(productViewModel);
        }

        [Route("edit-product/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id) return NotFound();

            var updatedProduct = await GetProduct(id);
            if (!ModelState.IsValid) return View(productViewModel);

            updatedProduct.Name = productViewModel.Name;
            updatedProduct.Description = productViewModel.Description;
            updatedProduct.Price = productViewModel.Price;
            updatedProduct.Active = productViewModel.Active;

            await _productService.Update(_mapper.Map<Product>(updatedProduct));

            return RedirectToAction("Index");
        }

        [Route("delete-product/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await GetProduct(id);

            if (product == null) return NotFound();

            return View(product);
        }

        [Route("delete-product/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await GetProduct(id);
            if (produto == null) return NotFound();

            await _productService.Remove(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ExportCsv()
        {
            var products = await _productRepository.GetAll();
            var csv = ConvertToCsv(products);

            var bytes = UnicodeEncoding.Unicode.GetBytes(csv);

            var result = new FileContentResult(bytes, "text/csv");
            result.FileDownloadName = "products.csv";
            return result;
        }

        private string ConvertToCsv(List<Product> list)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Id");
            sb.Append(",");
            sb.Append("Name");
            sb.Append(",");
            sb.Append("Description");
            sb.Append(",");
            sb.Append("Price");
            sb.Append(",");
            sb.Append("RegisterDate");
            sb.Append(",");
            sb.Append("Active");
            sb.AppendLine();

            foreach (var item in list)
            {
                sb.Append(item.Id);
                sb.Append(",");
                sb.Append(item.Name);
                sb.Append(",");
                sb.Append(item.Description);
                sb.Append(",");
                sb.Append(item.Price);
                sb.Append(",");
                sb.Append(item.RegisterDate.ToString("MM/dd/YYY HH:mm"));
                sb.Append(",");
                sb.Append(item.Active ? "Yes" : "No");
                sb.AppendLine();
            }

            return sb.ToString();
        }

        
        public async Task<IActionResult> ExportJson()
        {
            var products = await _productRepository.GetAll();

            string json = JsonConvert.SerializeObject(products, Formatting.Indented);

            var bytes = UnicodeEncoding.Unicode.GetBytes(json);

            var result = new FileContentResult(bytes, "text/json");
            result.FileDownloadName = "products.json";
            return result;
        }

        private async Task<ProductViewModel> GetProduct(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productRepository.GetProductById(id));

            return product;
        }
    }
}
