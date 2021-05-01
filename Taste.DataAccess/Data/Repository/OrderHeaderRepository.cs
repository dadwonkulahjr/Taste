using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;

namespace Taste.DataAccess.Data.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public OrderHeaderRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }
        public void Update(OrderHeader orderHeader)
        {
            OrderHeader orderFromDb = _applicationDbContext.OrderHeaders.Find(orderHeader.Id);
            if(orderHeader!= null)
            {
                _applicationDbContext.Update(orderFromDb);
                _applicationDbContext.SaveChanges();
            }
        }

    }

}
