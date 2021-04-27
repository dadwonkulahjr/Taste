using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;

namespace Taste.DataAccess.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CategoryRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            
        }
        public IEnumerable<SelectListItem> GetCategoryListForDropDown()
        {
            return _applicationDbContext.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
        }
        public void Update(Category categoryToUpdate)
        {
            //var result = _applicationDbContext.Attach(categoryToUpdate);
            //result.State = EntityState.Modified;
            Category findCategory = _applicationDbContext.Categories.Find(categoryToUpdate.Id);
            if(findCategory != null)
            {
                findCategory.Name = categoryToUpdate.Name;
                findCategory.DisplayOrder = categoryToUpdate.DisplayOrder;
                _applicationDbContext.SaveChanges();
            }
        }
    }
}
