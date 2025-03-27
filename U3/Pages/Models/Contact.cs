using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace U3.Pages.Models
{
    public class Contact

    {

        [BindProperty]

        [DisplayName("Your Id")]

        [Range(1, 100, ErrorMessage = "Invalid input")]
        public int ContactId { get; set; }

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]

        [DataType(DataType.Date)]

        [CustomBirthDate(ErrorMessage = "Birthdate is less than or equal to current date")]

        public DateTime DateOfBirth { get; set; }

        [BindProperty]
        [EmailAddress(ErrorMessage = "Invalid format entered")]
        public string Email { get; set; }
    }
}
