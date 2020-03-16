namespace SampleDBWebApis.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using SampleDBWebApis.DataLayer;

    public class BuildCustomersModelServices : DbContext, IBuildCustomersModelServices
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

        public Customer CreateNewCustomer(Customer custContext)
        {
            Context.Customers.Add(custContext);
            Context.SaveChanges();
            return custContext;
        }

        public List<Customer> GetListOfCustomers(string customerSearch)
        {
            return Context.Customers
                                   .Where(s => s.CompanyName.StartsWith(customerSearch))
                                   .ToList();
        }

        public Customer GetCustomer(string Id)
        {
            return Context.Customers
                               .Where(s => s.CustomerID == Id)
                               .FirstOrDefault<Customer>();
        }

        public int UpdateCustomer()
        {
            
            return Context.SaveChanges();
        }

        public int DeleteCustomer(string CustomerID)
        {
            Customer cust = GetCustomer(CustomerID);
            Context.Customers.Attach(cust);
            Context.Customers.Remove(cust);
            return Context.SaveChanges();
        }

        public List<CustomersOrderTotalByQuarter> GetCustomersOrderTotalByQuarter(int Year, int Quarter)
        {
            return Context.CustomersOrderTotalByQuarters
                               .Where(s => s.Year == Year && s.Quarter == Quarter)
                               .OrderBy(s => s.CompanyName)
                               .ToList<CustomersOrderTotalByQuarter>();
        }

    }
}
