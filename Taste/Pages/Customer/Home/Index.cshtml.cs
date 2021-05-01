using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;
using Taste.Models.ViewModels;
using Taste.Utility;

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

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if(claim != null)
            {
                int shoppingCartCount = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value).ToString().Count();

                HttpContext.Session.SetInt32(SD.ShoppingCart, shoppingCartCount);
            }

            CustomerHomeVM.ListOfMenuItems = _unitOfWork.MenuItem.GetAll(null, null, "Category,FoodType");
            CustomerHomeVM.ListOfCategorieItems = _unitOfWork.Category.GetAll(null, c => c.OrderBy(c => c.DisplayOrder), null);
         
        }
    }
}
