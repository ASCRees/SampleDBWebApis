using NUnit.Framework;
using SampleDBWebApis.Controllers;
using SampleDBWebApis.ModelBuilders;
using SampleDBWebApis.Service;
using SampleDBWebApis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDBWebApis.Tests.Base
{
    public class BaseCustomerTests : BaseTests
    {
        protected CustomersController customersController = null;
        protected IBuildCustomersModelServices customerModelsServices = null;

        protected DataLayer.Customer createdCustomerModel = new DataLayer.Customer
        {
            CustomerID = string.Empty,
            CompanyName = "Los Polos Hermanos",
            ContactName = "Gus",
            Phone = "555-2121",
            City = "Alburquerque",
            ContactTitle = "Don",
            Address = "1 Main Street",
            PostalCode = "76243",
            Region = "",
            Country = "USA"
        };

        protected string customerID = "TEST1";


        [SetUp]
      public virtual void Setup()
      {
            customerModelsServices = new BuildCustomersModelServices();

            customersController = new CustomersController(customerModelsServices, new CustomerModelBuilders(customerModelsServices));
      }

    }
}
