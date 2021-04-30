using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;
using Taste.Models.ViewModels;

namespace Taste.Pages.Customer.Home
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerHomeVM CustomerHomeVM { get; set; } = new();
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            CustomerHomeVM.ListOfMenuItems = _unitOfWork.MenuItem.GetAll(null, null, "Category,FoodType");
            CustomerHomeVM.ListOfCategorieItems = _unitOfWork.Category.GetAll(null, c => c.OrderBy(c => c.DisplayOrder), null);
         
        }
    }
}
