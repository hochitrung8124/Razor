using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.IO;
using U3.Pages.Models;
using U3.Pages.Services;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace U3.Pages
{
    public class ProductPageModel : PageModel
    {
        private IHostingEnvironment _environment;
        [BindProperty]
        public IFormFileCollection UploadedImages { get; set; }
        public string notification { get; set; }
        public List<Product> products = null;
        public Product product;
        public List<Product> products_search = null;
        public ProductService _productService;
        public ProductPageModel(ProductService productService, IHostingEnvironment environment)
        {
            products = productService.GetProducts();
            products_search = new List<Product>();
            _productService = productService;
            _environment = environment;

        }

        public ProductService Get_productService()
        {
            return _productService;
        }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                ViewData["Title"] = $"Thong tin sản phẩm (ID = {id.Value})";
                product = _productService.GetProductById(id.Value);
            }
            else
            {
                ViewData["Title"] = $"Danh sách sản phẩm";
            }
        }

        public IActionResult OnGetLastProduct()
        {
            ViewData["Title"] = $"Sản phẩm cuối";
            product = _productService.GetProducts().LastOrDefault();
            if (product != null)
            {
                return Page();
            }
            return NotFound();
        }

        public IActionResult OnGetRemoveAll()
        {
            products.Clear();
            return RedirectToPage("ProductPage");
        }
        public IActionResult OnPostAddProduct([FromForm] Product product)
        {
            if (UploadedImages != null)
            {
                if (UploadedImages != null && UploadedImages.Count > 0)
                {
                    foreach (var file in UploadedImages)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(file.FileName);
                        var savePath = Path.Combine("wwwroot/images", fileName);
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        product.ImagePaths.Add("/images/" + fileName);
                    }
                }
            }
            if (!ModelState.IsValid)
            {
                notification = "Invalid data";
                return Page();
            }

            _productService.addData(product);
            return RedirectToPage("ProductPage");
        }

        public IActionResult OnGetLoadAll()
        {
            _productService.LoadProducts();
            return RedirectToPage("ProductPage");
        }

        public IActionResult OnPostByName(string name)
        {
            ViewData["Title"] = "Tìm kiếm";
            products_search.Clear();
            var h = _productService.GetByName(name);
            products_search.AddRange(h);
            return Page();
        }

        public IActionResult OnPostUpdateProduct([FromForm] Product product)
        {
            _productService.updateData(product);
            return RedirectToPage("ProductPage");
        }
       
    }
}

