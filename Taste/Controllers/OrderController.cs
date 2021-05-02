using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;
using Taste.Models.ViewModels;
using Taste.Utility;

namespace Taste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get(string status=null)
        {
            List<OrderDetailsViewModel> orderDetailsViewModels = new();
            IEnumerable<OrderHeader> orderHeaderList;


            if (User.IsInRole(SD.CustomerRole))
            {
                var claimIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
                orderHeaderList = _unitOfWork.OrderHeader.GetAll(x => x.UserId == claim.Value, null, "ApplicationUser");
            }
            else
            {
                orderHeaderList = _unitOfWork.OrderHeader.GetAll(null, null, "ApplicationUser");
            }

            if(status == "cancelled")
            {
                orderHeaderList = orderHeaderList.Where(o => o.Status == SD.StatusCancelled || o.Status == SD.StatusRefounded || o.Status == SD.PaymentStatusRejected);

            }
            else
            {
                if(status == "completed")
                {
                    orderHeaderList = orderHeaderList.Where(o => o.Status == SD.StatusCompleted);
                }
                else
                {
                    orderHeaderList = orderHeaderList.Where(o => o.Status == SD.StatusReady || o.Status == SD.StatusInProcess || o.Status == SD.StatusSubmitted || o.Status == SD.PaymentStatusPending);
                }
            }
            foreach (OrderHeader item in orderHeaderList)
            {
                OrderDetailsViewModel orderDetailsViewModel = new()
                {
                    OrderHeader = item,
                    OrderDetail = _unitOfWork.OrderDetail.GetAll(o => o.OrderId == item.Id).ToList()
                };

                orderDetailsViewModels.Add(orderDetailsViewModel);
            }

            return Json(new { data = orderDetailsViewModels });
        }
    }
}
