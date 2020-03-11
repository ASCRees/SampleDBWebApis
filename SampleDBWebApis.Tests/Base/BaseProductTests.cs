using NUnit.Framework;
using SampleDBWebApis.Controllers;
using SampleDBWebApis.ModelBuilders;
using SampleDBWebApis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDBWebApis.Tests.Base
{
    public class BaseProductTests: BaseTests
    {
        public ProductsController productsController = null;
        public IBuildProductsModelsServices productModelsServices = null;

      [SetUp]
      public virtual void Setup()
      {
            productModelsServices = new BuildProductsModelsServices();

            productsController = new ProductsController(productModelsServices, new ProductModelBuilders(productModelsServices));
      }

    }
}
