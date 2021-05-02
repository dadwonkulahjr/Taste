using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;
using Taste.Models.ViewModels;
using Taste.Utility;

namespace Taste.Pages.Admin.Order
{
    public class OrderDetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderDetailsViewModel OrderDetailsViewModel { get; set; }
        public OrderDetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            OrderDetailsViewModel = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id),
                OrderDetail = _unitOfWork.OrderDetail.GetAll(x => x.OrderId == id).ToList()
            };

            OrderDetailsViewModel.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(a => a.Id == OrderDetailsViewModel.OrderHeader.UserId);
        }

        public IActionResult OnPostOrderConfirm(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == id);
            if (orderHeader != null)
            {
                orderHeader.Status = SD.StatusCompleted;
                _unitOfWork.Save();
                return RedirectToPage("OrderList");
            }
            return Page();
        }
        public IActionResult OnPostOrderReady(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == id);
            if (orderHeader != null)
            {
                orderHeader.Status = SD.StatusReady;
                _unitOfWork.Save();
                return RedirectToPage("OrderList");
            }
            return Page();
        }


        public IActionResult OnPostOrderCancel(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == id);
            if (orderHeader != null)
            {
                orderHeader.Status = SD.StatusCancelled;
                _unitOfWork.Save();
                return RedirectToPage("OrderList");
            }
            return Page();
        }

        public IActionResult OnPostOrderRefund(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == id);

            var options = new RefundCreateOptions()
            {
                Amount = Convert.ToInt32(orderHeader.OrderTotal * 100),
                Reason = RefundReasons.RequestedByCustomer
            };
            var service = new RefundService();
            Refund refund = service.Create(options);

            orderHeader.Status = SD.StatusRefounded;
            _unitOfWork.Save();
            return RedirectToPage("OrderList");

        }
    }
}
