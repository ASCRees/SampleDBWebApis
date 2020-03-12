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
        public CustomersController customersController = null;
        public IBuildCustomersModelServices customerModelsServices = null;

      [SetUp]
      public virtual void Setup()
      {
            customerModelsServices = new BuildCustomersModelServices();

            customersController = new CustomersController(customerModelsServices, new CustomerModelBuilders(customerModelsServices));
      }

    }
}
