using System.Collections.Generic;

namespace Taste.Models.ViewModels
{
    public class CustomerHomeVM
    {
        public CustomerHomeVM()
        {
            ListOfCategorieItems = new List<Category>();
            ListOfMenuItems = new List<MenuItem>();
        }
        public IEnumerable<MenuItem> ListOfMenuItems{get; set; }
        public IEnumerable<Category> ListOfCategorieItems { get; set; }
    }
}
