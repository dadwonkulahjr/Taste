using System;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;

namespace Taste.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            Category = new CategoryRepository(_applicationDbContext);
            FoodType = new FoodTypeRepository(_applicationDbContext);
            MenuItem = new MenuItemRepository(_applicationDbContext);
            ApplicationUser = new ApplicationUserRepository(_applicationDbContext);
            ShoppingCart = new ShoppingCartRepository(_applicationDbContext);
            OrderDetail = new OrderDetailRepository(_applicationDbContext);
            OrderHeader = new OrderHeaderRepository(_applicationDbContext);
            SP_Call = new SP_Call(_applicationDbContext);
        }
        public ICategoryRepository Category { get; private set; }

        public IFoodTypeRepository FoodType { get; private set; }

        public IMenuItemRepository MenuItem { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }

        public ISP_Call SP_Call { get; private set; }

        public void Dispose()
        {
            GC.SuppressFinalize(_applicationDbContext);

        }
        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
