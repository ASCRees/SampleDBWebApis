using SampleDBWebApis.DataLayer;
using System.Collections.Generic;

namespace SampleDBWebApis.Service
{
    public interface IBuildCustomersModelServices
    {
        SampleDBEntities Context { get; }

        Customer CreateNewCustomer(Customer custContext);
        List<Customer> GetListOfCustomers(string customerSearch);
        Customer GetCustomer(string Id);
        int UpdateCustomer();
        int DeleteCustomer(string CustomerID);
        List<CustomersOrderTotalByQuarter> GetCustomersOrderTotalByQuarter(int Year, int Quarter);
    }
}