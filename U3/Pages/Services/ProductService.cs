using Microsoft.EntityFrameworkCore;
using U3.Pages.Models;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace U3.Pages.Services
{
    public class ProductService
    {
        private MyDBc _myDBc;

        public ProductService(MyDBc myDBc) 
        {
            _myDBc = myDBc;
        }
        public async Task<List<Product>> GetProductsAsync()
        {
            await Task.Delay(500);
            return getProducts();
        }

        public List<Product> getProducts() 
        {
            var products = _myDBc.Products.Select(dd => new Product
            {
                Id = dd.Id,
                Name = dd.Name,
                Description = dd.Description,
                ImagePaths = dd.ImagePaths,
                Price = dd.Price,
                CategoryId = dd.CategoryId,
            });
            return products.ToList(); 
        }
        public Product GetProductById(int? id) 
        {
            var check = _myDBc.Products.FirstOrDefault(tt => tt.Id.Equals(id));
            if (check != null)
            {
                return check;
            }
                return null;
        }
        public void addProduct(Product data)
        {
            _myDBc.Add(data);
            _myDBc.SaveChanges();
        }

        public void deleteProduct(int id)
        {
            var check = _myDBc.Products.FirstOrDefault(tt => tt.Id.Equals(id));
            if (check != null)
            {
                _myDBc.Remove(check);
                _myDBc.SaveChanges();
            }
        }

        public void updateProduct(Product product) 
        {
            var check = _myDBc.Products.FirstOrDefault(tt => tt.Id.Equals(product.Id));
            if (check != null)
            {
                check.Name = product.Name;
                check.Description = product.Description;
                check.ImagePaths = product.ImagePaths;
                check.Price = product.Price;
                check.Stock = product.Stock;    
                _myDBc.SaveChanges();
            }
        }

        public List<Product> searchByName(string name) 
        {
            var _listProduct = new List<Product>();
            foreach (var item in _myDBc.Products)
            {
                if (item.Name.Equals(name))
                {
                    _listProduct.Add(item);
                }
            }
            return _listProduct.ToList();    
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _myDBc.Products.Where(p => p.CategoryId == categoryId).ToList();
        }
        public List<Category> GetCategories()
        {
            return _myDBc.Categories.ToList();
        }
    }
}


