using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Taste.DataAccess.Data.Repository.IRepository;

namespace Taste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public MenuItemController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _unitOfWork.MenuItem.GetAll(null, null, "Category,FoodType");
            return Json(new { data });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var obj = _unitOfWork.MenuItem.GetFirstOrDefault(c => c.Id == id);
                if (obj == null)
                {
                    return Json(new { success = false, message = "Error why deleting." });
                }
                string imageStringLen = obj.Image[18..];
                string upload = Path.Combine(_hostingEnvironment.WebRootPath, @"images\menuItems");

                string fullPath = Path.Combine(upload, imageStringLen);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                _unitOfWork.MenuItem.Remove(obj);
                _unitOfWork.Save();
            }
            catch (System.Exception)
            {
                return Json(new { success = false, message = "Error why deleting." });
            }
            return Json(new { success = true, message = "Delete successful" });

        }
    }
}
