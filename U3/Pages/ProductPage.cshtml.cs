using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using U3.Pages.Models;
using U3.Pages.Services;

namespace U3.Pages
{
    public class ProductPageModel : PageModel
    {
 
        [BindProperty]
        public IFormFileCollection UploadedImages { get; set; }
        public string notification { get; set; }
        public ProductService _productService;
        [BindProperty]
        public Product product { get; set; }

        [BindProperty]
        public int categoryId { get; set; }

        public List<Product> products { get; set; }
        public List<Category> categories { get; set; }
        public List<Product> products_search { get; set; }
        public string Message { get; set; }
        public bool IsProductDetail { get; set; }
        public ProductPageModel(ProductService productService)
        {
            _productService = productService;
            products = _productService.getProducts();
            categories = _productService.GetCategories();
            products_search = new List<Product>();
        }

        public void OnGet(int? id, int? categoryId)
        {
            if (id != null)
            {
                ViewData["Title"] = $"Thong tin sản phẩm (ID = {id.Value})";
                product = _productService.GetProductById(id.Value);
                IsProductDetail = true;
            }
            else
            {
                if (categoryId.HasValue && categoryId.Value > 0)
                {
                    this.categoryId = categoryId.Value;
                }
                else
                {
                    this.categoryId = 0;
                }
            }
        }

        public IActionResult OnPostUpdateProduct([FromForm] Product product)
        {
            _productService.updateProduct(product);
            return RedirectToPage("ProductPage");
        }
        public IActionResult OnPostDeleteProduct([FromForm] Product product)
        {
            _productService.deleteProduct(product.Id);
            return RedirectToPage("ProductPage", new { id = (int?)null });
        }

        public IActionResult OnGetProducts()
        {
            var _product = _productService.getProducts();
            if (_product != null)
            {
                return Page();
            }
            return NotFound();
        }

        public IActionResult OnGetRemoveAll()
        {
            _productService.RemoveAll();
            return RedirectToPage("ProductPage");
        }

        public void OnPostSearch(string searchName)
        {
            products_search = _productService.searchByName(searchName);
        }

        public void OnPostFilterByCategory(int categoryId)
        {
            products_search = _productService.GetProductsByCategory(categoryId);
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

            _productService.addProduct(product);
            return RedirectToPage("ProductPage");
        }



      
    }
}

