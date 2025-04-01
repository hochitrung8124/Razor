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
        }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                ViewData["Title"] = $"Thong tin sản phẩm (ID = {id.Value})";
                product = _productService.GetProductById(id.Value);
                IsProductDetail = true;
            }
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

            _productService.addProduct(product);
            return RedirectToPage("ProductPage");
        }



      
    }
}

