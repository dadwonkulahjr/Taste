using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;

namespace Taste.DataAccess.Data.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public OrderDetailRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }

        public void Update(OrderDetail orderDetail)
        {
            OrderDetail orderFromDb = _applicationDbContext.OrderDetails.Find(orderDetail.Id);
            if (orderDetail != null)
            {
                _applicationDbContext.Update(orderFromDb);
                _applicationDbContext.SaveChanges();
            }
        }
    }

}
