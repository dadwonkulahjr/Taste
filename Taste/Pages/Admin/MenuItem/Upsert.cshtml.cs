using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models.ViewModels;
using Taste.Utility;

namespace Taste.Pages.Admin.MenuItem
{
    [Authorize(Roles = SD.ManagerRole)]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvirnment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvirnment;
        }
        [BindProperty]
        public MenuItemVM MenuItemVM { get; set; }
        public IActionResult OnGet(int? id)
        {
            MenuItemVM = new()
            {
                CategoryDropdownList = _unitOfWork.Category.GetCategoryListForDropDown(),
                FoodTypeDropdownList = _unitOfWork.FoodType.GetFoodTypeListForDropDown()
            };
            if (id != null)
            {
                MenuItemVM.MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(m => m.Id == id);
                if (MenuItemVM.MenuItem == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (MenuItemVM.MenuItem.Id == 0)
            {
                string newGuid = Guid.NewGuid().ToString();
                string upload = Path.Combine(webRootPath, @"images\menuItems");
                string extention = Path.GetExtension(files[0].FileName);
                string fileName = files[0].FileName;
                string combine = fileName + "__" + newGuid + extention;

                using (var fileStream = new FileStream(Path.Combine(upload, combine), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                MenuItemVM.MenuItem.Image = @"\images\menuItems\" + combine;
                _unitOfWork.MenuItem.Add(MenuItemVM.MenuItem);
            }
            else
            {
                var findObjFromDb = _unitOfWork.MenuItem.Get(MenuItemVM.MenuItem.Id);
                if (findObjFromDb != null)
                {
                    if (files.Count > 0)
                    {
                        string imageStringLen = findObjFromDb.Image[18..];
                       
                        string newGuid = Guid.NewGuid().ToString();
                        string upload = Path.Combine(webRootPath, @"images\menuItems");
                        string extention = Path.GetExtension(files[0].FileName);

                        string fullPath = Path.Combine(upload, imageStringLen);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }

                        string fileName = files[0].FileName;
                        string combineNew = fileName + "__" + newGuid + extention;

                        using (var fileStream = new FileStream(Path.Combine(upload, combineNew), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        MenuItemVM.MenuItem.Image = @"/images/menuItems/" + combineNew;

                    }
                    else
                    {
                        MenuItemVM.MenuItem.Image = findObjFromDb.Image;
                    }

                    _unitOfWork.MenuItem.Update(MenuItemVM.MenuItem);
                }
                else
                {

                    return RedirectToPage("./Index");
                }



            }



            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}
