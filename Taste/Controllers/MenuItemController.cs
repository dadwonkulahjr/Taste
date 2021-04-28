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
            return Json(new { data = _unitOfWork.Category.GetAll(null, null, "Category,FoodType") });
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

                string imagePath = Path.Combine(_hostingEnvironment.WebRootPath, obj.Image.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
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
