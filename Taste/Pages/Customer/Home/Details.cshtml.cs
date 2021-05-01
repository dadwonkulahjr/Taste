using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Security.Claims;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;
using Taste.Utility;

namespace Taste.Pages.Customer.Home
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public ShoppingCart ShoppingCartObj { get; set; }
        public void OnGet(int id)
        {
            ShoppingCartObj = new()
            {
                MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(includedProperties: "Category,FoodType",
                filter: c => c.Id == id),
                MenuItemId = id
            };
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                ShoppingCartObj.ApplicationUserId = claim.Value;
                ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId &&
                 c.MenuItemId == ShoppingCartObj.MenuItemId);

                if(shoppingCartFromDb == null)
                {
                    _unitOfWork.ShoppingCart.Add(ShoppingCartObj);
                }
                else
                {
                  _unitOfWork.ShoppingCart.IncrementCount(shoppingCartFromDb, ShoppingCartObj.Count);
                }
                _unitOfWork.Save();
                var count = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId).ToList().Count();
                HttpContext.Session.SetInt32(SD.ShoppingCart, count);
                return RedirectToPage("Index");
            }
            else
            {
                ShoppingCartObj.MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(includedProperties: "Category,FoodType",
                filter: c => c.Id == ShoppingCartObj.MenuItemId);
                return Page();
            }
        }
    }
}
