using AutoMapper;
using SampleDBWebApis.Models;
using SampleDBWebApis.Service;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleDBWebApis.ModelBuilders
{
    public class CustomerModelBuilders : ICustomerModelBuilders
    {
        private IBuildCustomersModelServices _buildModelsService;

        [DefaultConstructor]
        public CustomerModelBuilders(IBuildCustomersModelServices buildModelsService)
        {
            _buildModelsService = buildModelsService;
        }

        public CustomerViewModel BuildPutCustomerModel(CustomerViewModel customerModel)
        {
            var custContext = _buildModelsService.GetCustomer(customerModel.CustomerID);
            if (custContext != null)
            {
                custContext.CompanyName = customerModel.CompanyName;
                custContext.ContactName = customerModel.ContactName;
                custContext.ContactTitle = customerModel.ContactTitle;
                custContext.Address = customerModel.Address;
                custContext.City = customerModel.City;
                custContext.Region = customerModel.Region;
                custContext.PostalCode = customerModel.PostalCode;
                custContext.Country = customerModel.Country;
                custContext.Phone = customerModel.Phone;
                custContext.Fax = customerModel.Fax;
                _buildModelsService.UpdateCustomer();
                return Mapper.Map<CustomerViewModel>(custContext);
            }
            else
            {
                return new CustomerViewModel();
            }
        }
    }
}