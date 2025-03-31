namespace U3.Pages.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Productaa> Products { get; set; } = new List<Productaa>();
    }
}
