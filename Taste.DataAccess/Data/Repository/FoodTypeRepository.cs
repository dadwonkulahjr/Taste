using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;

namespace Taste.DataAccess.Data.Repository
{
    public class FoodTypeRepository : Repository<FoodType>, IFoodTypeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public FoodTypeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IEnumerable<SelectListItem> GetFoodTypeListForDropDown()
        {
            return _applicationDbContext.FoodTypes.Select(f => new SelectListItem()
            {
                Text = f.Name,
                Value = f.Id.ToString()
            });
        }

        public void Update(FoodType foodTypeToUpdate)
        {
            var findFoodToEdit = _applicationDbContext.FoodTypes.Find(foodTypeToUpdate.Id);
            if(findFoodToEdit != null)
            {
                findFoodToEdit.Name = foodTypeToUpdate.Name;
                _applicationDbContext.SaveChanges();
            }
        }
    }
}
