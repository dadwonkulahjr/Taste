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
        }
        public ICategoryRepository Category { get; private set; }
        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
