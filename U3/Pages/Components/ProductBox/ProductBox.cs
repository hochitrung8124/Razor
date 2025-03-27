using Microsoft.AspNetCore.Mvc;
using U3.Pages.Models;
using U3.Pages.Services;

namespace U3.Pages.Components.ProductBox
{
    public class ProductBox : ViewComponent
    {
        /*        List<Product> products = new List<Product>()
                {
                    new Product() { Name = "Ip14", Description = "Dien thoai", Price = 1000 },
                    new Product() { Name = "Ip15", Description = "Dien thoai", Price = 1000 },
                    new Product() { Name = "Ip16", Description = "Dien thoai", Price = 1000 }
                };*/

        /*        public IViewComponentResult Invoke()
                {

                    //nếu muốn thay file mac định "default.cshtml" thay thành một file bất kì thì dùng cấu trúc
                    return View<List<Product>>("_Default", products);

                    //mặc đinh
                    return View<List<Product>>(products);
                }*/
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
