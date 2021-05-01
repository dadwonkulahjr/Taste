using Microsoft.AspNetCore.Mvc;
using System;
using Taste.DataAccess.Data.Repository.IRepository;

namespace Taste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.ApplicationUser.GetAll() });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var obj = _unitOfWork.ApplicationUser.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error why Locking/Unlocking." });
            }

            _unitOfWork.ApplicationUser.LockUser(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Operation Successful!" });

        }
    }
}
