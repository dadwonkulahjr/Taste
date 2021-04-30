using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Taste.Models.ViewModels
{
    public class MenuItemVM
    {
        public IEnumerable<SelectListItem> CategoryDropdownList  { get; set; }
        public IEnumerable<SelectListItem> FoodTypeDropdownList { get; set; }
        public MenuItem MenuItem { get; set; } = new();
    }
}
