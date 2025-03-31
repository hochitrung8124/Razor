using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace U3.Pages.Models
{
    public class Productlast
    {
        [BindProperty]
        [DisplayName("Your Id")]
        [Range(1, 100, ErrorMessage = "Invalid input")]
        public int Id { get; set; }
        
        [DisplayName("Your Name")]
        [BindProperty]
        [Required(ErrorMessage = "Tên không được để trống")]

        public string Name { get; set; }

        [BindProperty]
        public string Description { get; set; }
        
        [BindProperty]
        [Range(1, 1000, ErrorMessage = "Invalid input")]
        public decimal Price { get; set; }
        public List<string> ImagePaths { get; set; } = new List<string>();
    }
}
