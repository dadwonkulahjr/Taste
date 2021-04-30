using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;

namespace Taste.DataAccess.Data.Repository
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public MenuItemRepository(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Update(MenuItem menuItemToUpdate)
        {
            var findMenuItemInDb = _applicationDbContext.MenuItems.Find(menuItemToUpdate.Id);
            if(findMenuItemInDb!= null)
            {
                findMenuItemInDb.Name = menuItemToUpdate.Name;
                findMenuItemInDb.Price = menuItemToUpdate.Price;
                findMenuItemInDb.Description = menuItemToUpdate.Description;
                findMenuItemInDb.CategoryId = menuItemToUpdate.CategoryId;
                findMenuItemInDb.FoodTypeId = menuItemToUpdate.FoodTypeId;
                if(findMenuItemInDb.Image != null)
                {
                    findMenuItemInDb.Image = menuItemToUpdate.Image;
                }

            
            }
        }
    }
}
