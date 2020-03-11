using SampleDBWebApis.DataLayer;
using System.Collections.Generic;

namespace SampleDBWebApis.Service
{
    public interface IBuildOrdersModelServices
    {
        SampleDBEntities Context { get; }

        Order CreateNewOrder(Order custContext);
        int DeleteOrder(int OrderID);
        Order GetOrder(int Id);
        List<OrdersByProductID> GetOrdersByProductId(int ProductId);
        int UpdateOrder();
    }
}