using SampleDBWebApis.Models;

namespace SampleDBWebApis.ModelBuilders
{
    public interface ICustomerModelBuilders
    {
        CustomerViewModel BuildPutCustomerModel(CustomerViewModel customerModel);
    }
}