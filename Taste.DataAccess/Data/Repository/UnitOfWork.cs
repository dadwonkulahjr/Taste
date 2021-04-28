﻿using System;
using Taste.DataAccess.Data.Repository.IRepository;

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
        }
        public ICategoryRepository Category { get; private set; }

        public IFoodTypeRepository FoodType { get; private set; }

        public IMenuItemRepository MenuItem { get; private set; }

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
