using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using U3.Pages.Models;

namespace U3.Pages
{
    public class ContactPageModel : PageModel
    {

        [BindProperty]
        public Contact contact { get; set; }
        public ContactPageModel()

        {

            contact = new Contact();

        }

public string notification { get; set; }
        public void OnPost()
        {

            if (ModelState.IsValid)

            {

                notification = "Valid incoming data";

            }

            else

            {

                notification = "Invalid data";

            }

        }
        public void OnGet()
        {
        }
    }
}
