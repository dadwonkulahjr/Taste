using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;
using Taste.Models.ViewModels;
using Taste.Utility;

namespace Taste.Pages.Customer.Cart
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderDetailsCartVM OrderDetailsCartVMObj { get; set; }
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            OrderDetailsCartVMObj = new()
            {
                OrderHeader = new()
            };

            OrderDetailsCartVMObj.OrderHeader.OrderTotal = 0;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if(claim != null)
            {
                IEnumerable<ShoppingCart> listOfShoppingCarts = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value);

                if(listOfShoppingCarts != null)
                {
                    OrderDetailsCartVMObj.ListOfShoppingCart = listOfShoppingCarts.ToList();
                }
                foreach (var cartList in OrderDetailsCartVMObj.ListOfShoppingCart)
                {
                    cartList.MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(c => c.Id == cartList.MenuItemId);
                    OrderDetailsCartVMObj.OrderHeader.OrderTotal += (cartList.MenuItem.Price * cartList.Count);
                }
            }
        }
        public IActionResult OnPostPlus(int id)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.Id == id);
            if(cart != null)
            {
                _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
                _unitOfWork.Save();
                return RedirectToPage("/Customer/Cart/Index");
            }
            return Page();
        }
        public IActionResult OnPostMinus(int id)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.Id == id);
            if (cart.Count == 1)
            {
                _unitOfWork.ShoppingCart.Remove(cart);
                _unitOfWork.Save();

                var shoppingCardCount = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == cart.ApplicationUserId).ToList().Count();

                HttpContext.Session.SetInt32(SD.ShoppingCart, shoppingCardCount);
            }
            else
            {
                _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
                _unitOfWork.Save();
            }
            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostRemove(int id)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.Id == id);
            if (cart != null)
            {
                _unitOfWork.ShoppingCart.Remove(cart);
                _unitOfWork.Save();

                var shoppingCardCount = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == cart.ApplicationUserId).ToList().Count();

                HttpContext.Session.SetInt32(SD.ShoppingCart, shoppingCardCount);
                return RedirectToPage("/Customer/Cart/Index");
            }
            return NotFound();
        }
    }
}
