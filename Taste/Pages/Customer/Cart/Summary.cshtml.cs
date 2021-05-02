using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;
using Taste.Models.ViewModels;
using Taste.Utility;

namespace Taste.Pages.Customer.Cart
{
    public class SummaryModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderDetailsCartVM OrderDetailsCart { get; set; }
        public SummaryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult OnGet()
        {
            OrderDetailsCart = new()
            {
                OrderHeader = new()
            };

            OrderDetailsCart.OrderHeader.OrderTotal = 0;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                IEnumerable<ShoppingCart> listOfShoppingCarts = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value);

                if (listOfShoppingCarts != null)
                {
                    OrderDetailsCart.ListOfShoppingCart = listOfShoppingCarts.ToList();
                }
                foreach (var cartList in OrderDetailsCart.ListOfShoppingCart)
                {
                    cartList.MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(c => c.Id == cartList.MenuItemId);
                    OrderDetailsCart.OrderHeader.OrderTotal += (cartList.MenuItem.Price * cartList.Count);
                }

                ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(a => a.Id == claim.Value);
                if (applicationUser != null)
                {
                    OrderDetailsCart.OrderHeader.PickupName = applicationUser.FullName;
                    OrderDetailsCart.OrderHeader.PickupTime = DateTime.Now;
                    OrderDetailsCart.OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
                    return Page();
                }
                else
                {
                    return NotFound();
                }

            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult OnPost(string stripeToken)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            OrderDetailsCart.ListOfShoppingCart = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value).ToList();

            OrderDetailsCart.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            OrderDetailsCart.OrderHeader.OrderDate = DateTime.Now;
            OrderDetailsCart.OrderHeader.UserId = claim.Value;
            OrderDetailsCart.OrderHeader.Status = SD.PaymentStatusPending;
            OrderDetailsCart.OrderHeader.PickupTime = Convert.ToDateTime(OrderDetailsCart.OrderHeader.PickupDate.ToShortDateString() + " " +
                OrderDetailsCart.OrderHeader.PickupTime.ToShortTimeString());

            List<OrderDetail> orderDetailsList = new();
            _unitOfWork.OrderHeader.Add(OrderDetailsCart.OrderHeader);
            _unitOfWork.Save();
            foreach (var item in OrderDetailsCart.ListOfShoppingCart)
            {
                item.MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == item.Id);
                OrderDetail orderDetail = new()
                {
                    MenuItemId = item.MenuItemId,
                    OrderId = OrderDetailsCart.OrderHeader.Id,
                    Description = item.MenuItem.Description,
                    Name = item.MenuItem.Name,
                    Price = item.MenuItem.Price,
                    Count = item.Count
                };

                OrderDetailsCart.OrderHeader.OrderTotal += (orderDetail.Count * orderDetail.Price);
                _unitOfWork.OrderDetail.Add(orderDetail);

            }
            OrderDetailsCart.OrderHeader.OrderTotal = Convert.ToDouble(string.Format("{0:##}", OrderDetailsCart.OrderHeader.OrderTotal));

            _unitOfWork.ShoppingCart.RemoveRange(OrderDetailsCart.ListOfShoppingCart);
            HttpContext.Session.SetInt32(SD.ShoppingCart, 0);
            _unitOfWork.Save();

            if (stripeToken != null)
            {
                var options = new ChargeCreateOptions()
                {
                    //Amount is in cents...
                    Amount = Convert.ToInt32(OrderDetailsCart.OrderHeader.OrderTotal * 100),
                    Currency = "usd",
                    Description = "Order ID: " + OrderDetailsCart.OrderHeader.Id,
                    Source = stripeToken
                };

                var service = new ChargeService();
                Charge charge = service.Create(options);

                OrderDetailsCart.OrderHeader.TransactionId = charge.Id;

                if (charge.Status.ToLower() == "succeeded")
                {
                    //Email
                    OrderDetailsCart.OrderHeader.PaymentStatus = SD.PaymentStatusApproved;
                    OrderDetailsCart.OrderHeader.Status = SD.StatusSubmitted;
                }
                else
                {
                    OrderDetailsCart.OrderHeader.Status = SD.PaymentStatusRejected;
                }
            }
            else
            {
                OrderDetailsCart.OrderHeader.PaymentStatus = SD.PaymentStatusRejected;
            }
            _unitOfWork.Save();
            return RedirectToPage("/Customer/Cart/OrderConfirmation", new { id = OrderDetailsCart.OrderHeader.Id });
        }
    }
}
