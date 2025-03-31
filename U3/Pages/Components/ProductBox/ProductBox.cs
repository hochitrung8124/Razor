using Microsoft.AspNetCore.Mvc;
using U3.Pages.Models;
using U3.Pages.Services;

namespace U3.Pages.Components.ProductBox
{
    public class ProductBox : ViewComponent
    {
         List<Product> products = null;
        public ProductBox(ProductService productService)
        {
            _productService = productService;
        }
        private readonly ProductService _productService;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            products = await _productService.GetProductsAsync();
            return View(products);
        }

    }
}
