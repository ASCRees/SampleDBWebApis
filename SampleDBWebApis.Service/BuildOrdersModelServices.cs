namespace SampleDBWebApis.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using SampleDBWebApis.DataLayer;

    public class BuildOrdersModelServices : DbContext, IBuildOrdersModelServices
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

        public Order CreateNewOrder(Order custContext)
        {
            Context.Orders.Add(custContext);
            Context.SaveChanges();
            return custContext;
        }


        public Order GetOrder(int Id)
        {
            return Context.Orders
                               .Where(s => s.OrderID == Id)
                               .FirstOrDefault<Order>();
        }

        public int UpdateOrder()
        {
            return Context.SaveChanges();
        }

        public int DeleteOrder(int OrderID)
        {
            Order cust = GetOrder(OrderID);
            Context.Orders.Attach(cust);
            Context.Orders.Remove(cust);
            return Context.SaveChanges();
        }

        public List<OrdersByProductID> GetOrdersByProductId(int ProductId)
        {
            return Context.OrdersByProductIDs
                               .Where(s => s.ProductID == ProductId)
                               .OrderBy(s => s.OrderDate)
                               .ThenBy(s => s.ProductName)
                               .ToList<OrdersByProductID>();
        }
    }
}
