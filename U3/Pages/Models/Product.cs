namespace U3.Pages.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<string> ImagePaths { get; set; } = new List<string>();
        public double Price { get; set; }
        public int Stock { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
