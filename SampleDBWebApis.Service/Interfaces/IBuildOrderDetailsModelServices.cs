using SampleDBWebApis.DataLayer;
using System.Collections.Generic;

namespace SampleDBWebApis.Service
{
    public interface IBuildOrderDetailsModelServices
    {
        SampleDBEntities Context { get; }

        OrderDetail CreateNewOrderDetails(OrderDetail orderContext);
        int DeleteOrderDetails(int OrderId, int ProductId);
        OrderDetail GetOrderDetailsByOrderIDAndProductID(int OrderId, int ProductId);
        List<OrderDetail> GetOrderDetailsForAnOrder(int Id);
        int UpdateOrderDetails();
    }
}