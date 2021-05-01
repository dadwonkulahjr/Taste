using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Taste.Utility;

namespace Taste.Pages.Admin.Users
{
    [Authorize(Roles = SD.ManagerRole)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
