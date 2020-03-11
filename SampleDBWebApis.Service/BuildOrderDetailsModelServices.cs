namespace SampleDBWebApis.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using SampleDBWebApis.DataLayer;

    public class BuildOrderDetailsModelServices : DbContext, IBuildOrderDetailsModelServices
    {
        SampleDBEntities _context;

        public SampleDBEntities Context
        {
            get
            {
                if (_context == null)
                {
                    _context = new SampleDBEntities();
                }
                return _context;
            }
        }

        public OrderDetail CreateNewOrderDetails(OrderDetail orderContext)
        {
            Context.OrderDetails.Add(orderContext);
            Context.SaveChanges();
            return orderContext;
        }

        public OrderDetail GetOrderDetailsByOrderIDAndProductID(int OrderId, int ProductId)
        {
            return Context.OrderDetails
                               .Where(s => s.ProductID == ProductId && s.OrderID == OrderId)
                               .FirstOrDefault<OrderDetail>();
        }

        public List<OrderDetail> GetOrderDetailsForAnOrder(int Id)
        {
            return Context.OrderDetails
                               .Where(s => s.OrderID == Id)
                               .ToList<OrderDetail>();
        }

        public int UpdateOrderDetails()
        {
            return Context.SaveChanges();
        }

        public int DeleteOrderDetails(int OrderId, int ProductId)
        {
            OrderDetail ord = GetOrderDetailsByOrderIDAndProductID(OrderId, ProductId);
            Context.OrderDetails.Attach(ord);
            Context.OrderDetails.Remove(ord);
            return Context.SaveChanges();
        }

    }
}
