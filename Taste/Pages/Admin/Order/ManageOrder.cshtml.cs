using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;
using Taste.Models.ViewModels;
using Taste.Utility;

namespace Taste.Pages.Admin.Order
{
    public class ManageOrderModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        public List<OrderDetailsViewModel> OrderDetailsVM { get; set; }
        public ManageOrderModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            OrderDetailsVM = new();
            List<OrderHeader> listOfOrderHeaders = _unitOfWork.OrderHeader.GetAll(o => o.Status == SD.StatusSubmitted || o.Status == SD.StatusInProcess)
                .OrderByDescending(o => o.PickupTime).ToList();

            foreach (var item in listOfOrderHeaders)
            {
                OrderDetailsViewModel model = new()
                {
                    OrderHeader = item,
                    OrderDetail = _unitOfWork.OrderDetail.GetAll(x => x.OrderId == item.Id).ToList()
                };

                OrderDetailsVM.Add(model);
            }
        }

        public IActionResult OnPostOrderPrepare(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == id);
            if(orderHeader != null)
            {
                orderHeader.Status = SD.StatusInProcess;
                _unitOfWork.Save();
                return RedirectToPage("ManageOrder");
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
                return RedirectToPage("ManageOrder");
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
                return RedirectToPage("ManageOrder");
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
            return RedirectToPage("ManageOrder");
            //if (orderHeader != null)
            //{
            //    orderHeader.Status = SD.StatusRefounded;
            //    _unitOfWork.Save();
            //    return RedirectToPage("ManageOrder");
            //}
            //return Page();
            
        }
    }
}
