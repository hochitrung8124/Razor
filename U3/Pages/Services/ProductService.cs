using U3.Pages.Models;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace U3.Pages.Services
{
    public class ProductService
    {
        public List<Product> Results { get; set; }
        private List<Product> products = null;
        private void listSP()
        {
            products = new List<Product>()
                {
                    new Product() { Id = 0, Name = "Ip14", Description = "Dien thoai", Price = 1000 },
                    new Product() { Id = 1, Name = "Ip15", Description = "Dien thoai", Price = 1000 },
                    new Product() {Id = 2, Name = "Ip16", Description = "Dien thoai", Price = 1000 }
                };
        }
        public List<Product> GetProducts() { return products; }

        public async Task<List<Product>> GetProductsAsync()
        {
            await Task.Delay(500);
            return LoadProducts();
        }
        public Product GetProductById(int id) 
        {
            foreach (Product product in products)
            {
                if(product.Id.Equals(id))
                {
                    return product;
                }
            }
            return null; 
        }
        public int GetNextId()
        {
            return products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
        }
        public void addData(Product data)
        {
            products.Add(data);
        }

        public void updateData(Product data)
        {
            var h = products.FirstOrDefault(t => t.Id.Equals(data.Id));
            if (h == null)
                return; 
            h.Name = data.Name; // có thể
            h.Price = data.Price;
            h.Description = data.Description;
        }

        public void deleteData(string name)
        {
            var h = products.FirstOrDefault(t => t.Name.Equals(name));
            if (h == null)
                return;
            products.Remove(h);
        }

        public  List<Product> LoadProducts()
        {

            listSP();
            return products;
        }

        public List<Product> GetByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Results = products
                    .Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            return Results;
        }
    }
}


