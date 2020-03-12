using System.Net;
using FluentAssertions;
using SampleDBWebApis.Services;
using NUnit.Framework;
using SampleDBWebApis.Controllers;
using SampleDBWebApis.Models;
using SampleDBWebApis.App_Start;
using SampleDBWebApis.Tests.Base;
using SampleDBWebApis.ModelBuilders;
using System;
using Moq;
using SampleDBWebApis.Service;

namespace SampleDBWebApis.Tests.Controllers
{
    public class CustomersControllerTests:BaseCustomerTests
    {
        [SetUp]
        public override void Setup()
        {
            AutoMapperConfig.CreateMappings();
            base.Setup();
        }

        [Test]
        public void Get_Customer_By_ID_With_DB()
        {
            // Arrange
            var customerID = "ALFKI";

            // Act
            var getSingleCustomerResponse = customersController.GetSingleCustomer(customerID);
            var customerViewModel = (CustomerViewModel)((System.Net.Http.ObjectContent)getSingleCustomerResponse.Content).Value;

            // Assert
            customerViewModel.CustomerID.Should().Be(customerID);

        }

        [Test]
        public void Get_Customer_By_ID_No_DB()
        {
            // Arrange
            var customerID = "TEST99";

            CustomersController customersController = null;

            // Mock the GetCustomer

            var mockcustomerModelsServices = new Mock<IBuildCustomersModelServices>();
            mockcustomerModelsServices.Setup(x => x.GetCustomer(It.IsAny<string>())).Returns(new DataLayer.Customer
            {
                CustomerID = customerID,
                CompanyName="Los Polos Hermanos",
                ContactName="Gus",
                Phone="555-2121",
                City="Alburquerque",
                ContactTitle="Don"
            });
            
            customersController = new CustomersController(mockcustomerModelsServices.Object, new CustomerModelBuilders(mockcustomerModelsServices.Object));

            // Act
            var getSingleCustomerResponse = customersController.GetSingleCustomer(customerID);
            var customerViewModel = (CustomerViewModel)((System.Net.Http.ObjectContent)getSingleCustomerResponse.Content).Value;

            // Assert
            customerViewModel.CustomerID.Should().Be(customerID);

        }

        [Test]
        public void Get_Customer_By_Starting_String()
        {
            // Arrange
            var customerString = "M";

            // Act
            CustomersViewModel customerViewModel = customersController.GetMultipleCustomers(customerString);

            // Assert
            customerViewModel.Customers.Count.Should().BeGreaterThan(0);

        }

        [Test]
        public void PutCustomer_Update_Existing_Customer()
        {
            // Arrange

            var customerID = "ALFKI";

            var getSingleCustomerResponse = customersController.GetSingleCustomer(customerID);
            CustomerViewModel customerViewModel = (CustomerViewModel)((System.Net.Http.ObjectContent)getSingleCustomerResponse.Content).Value;
 
            var companyName = "Company Update " + DateTime.Now.ToString();

            customerViewModel.CompanyName = companyName;

            // Act
            var customerHttpResponse = customersController.PutCustomer(customerViewModel);
            var customerResultViewModel = (CustomerViewModel)((System.Net.Http.ObjectContent)customerHttpResponse.Content).Value;

            // Assert
            customerResultViewModel.CompanyName.Should().BeEquivalentTo(companyName);

        }

        //[Test]
        //public void PostProduct_Create_New_Product()
        //{
        //    // Arrange
        //    ProductViewModel product = new ProductViewModel
        //    {
        //        ProductName = "Tomatoe Ketchup",
        //        UnitPrice = 1.59m,
        //        UnitsInStock = 200,
        //        SupplierID = 1,
        //        CategoryID = 1,
        //        UnitsOnOrder = 0
        //    };

        //    // Act
        //    var productHttpResponse = customersController.PostProduct(product);
        //    var productViewModel =(ProductViewModel)((System.Net.Http.ObjectContent)productHttpResponse.Content).Value;

        //    // Assert
        //    productViewModel.ProductID.Should().BeGreaterThan(0);

        //}

        //[Test]
        //public void PutProduct_Update_Product()
        //{
        //    // Arrange
        //    ProductViewModel product = new ProductViewModel
        //    {
        //        ProductID=84,
        //        ProductName = "BBQ Sauce",
        //        UnitPrice = 1.39m,
        //        UnitsInStock = 90,
        //        SupplierID = 1,
        //        CategoryID = 1,
        //        UnitsOnOrder = 100,

        //    };

        //    // Act
        //    var productHttpResponse = customersController.PutProduct(product);

        //    var productViewModel = (ProductViewModel)((System.Net.Http.ObjectContent)productHttpResponse.Content).Value;

        //    // Assert
        //    productViewModel.UnitsInStock.Should().Be(90);

        //}

        //[Test]
        //public void PutProduct_Update_Product_NotFound()
        //{
        //    // Arrange
        //    ProductViewModel product = new ProductViewModel
        //    {
        //        ProductID = 191919191,
        //        ProductName = "BBQ Sauce",
        //        UnitPrice = 1.39m,
        //        UnitsInStock = 90,
        //        SupplierID = 1,
        //        CategoryID = 1,
        //        UnitsOnOrder = 100,

        //    };

        //    // Act
        //    var productHttpResponse = customersController.PutProduct(product);

        //    // Assert
        //    productHttpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

        //}

        //[Test]
        //public void PutProduct_Update_Product_Model_Not_Valid()
        //{
        //    // Arrange
        //    ProductViewModel product = new ProductViewModel
        //    {
        //        ProductID = 191919191,
        //        ProductName = string.Empty,
        //        UnitPrice = 1.39m,
        //        UnitsInStock = 90,
        //        SupplierID = 1,
        //        CategoryID = 1,
        //        UnitsOnOrder = 100,

        //    };

        //    customersController.ModelState.AddModelError("ProductName", "Is required");
        //    // Act
        //    var productHttpResponse = customersController.PutProduct(product);

        //    // Assert
        //    productHttpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        //}

        //[Test]
        //public void PutProduct_Create_Product()
        //{
        //    // Arrange
        //    ProductViewModel product = new ProductViewModel
        //    {
        //        ProductName = "Salad Cream",
        //        UnitPrice = 2.39m,
        //        UnitsInStock = 190,
        //        SupplierID = 1,
        //        CategoryID = 1,
        //        UnitsOnOrder = 50,
        //        QuantityPerUnit="10",
        //        ReorderLevel=20,
        //        Discontinued=false

        //    };

        //    // Act
        //    var productHttpResponse = customersController.PutProduct(product);

        //    // Assert
        //    productHttpResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        //}

        //[Test]
        //public void PatchProduct_Update_Product()
        //{
        //    // Arrange
        //    ProductPatchViewModel product = new ProductPatchViewModel
        //    {
        //        ProductID = 84,
        //        ProductName = "BullsEye BBQ Sauce",
        //        UnitPrice = 1.19m,
        //        UnitsInStock = 91,
        //        UnitsOnOrder = 11,
        //        ReorderLevel=5

        //    };


        //    // Act
        //    var productHttpResponse = customersController.PatchProduct(product);

        //    var productViewModel = (ProductPatchViewModel)((System.Net.Http.ObjectContent)productHttpResponse.Content).Value;

        //    // Assert
        //    productViewModel.UnitsInStock.Should().Be(91);

        //}

        //[Test]
        //public void PatchProduct_Update_Product_Not_Found()
        //{
        //    // Arrange
        //    ProductPatchViewModel product = new ProductPatchViewModel
        //    {
        //        ProductID = 19191919,
        //        ProductName = "BullsEye BBQ Sauce",
        //        UnitPrice = 1.19m,
        //        UnitsInStock = 91,
        //        UnitsOnOrder = 11,
        //        ReorderLevel = 5

        //    };


        //    // Act
        //    var productHttpResponse = customersController.PatchProduct(product);

        //    var productViewModel = (ProductPatchViewModel)((System.Net.Http.ObjectContent)productHttpResponse.Content).Value;

        //    // Assert
        //    productHttpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

        //}

        //[Test]
        //public void PatchProduct_Update_Product_Model_Not_Valid()
        //{
        //    // Arrange
        //    ProductPatchViewModel product = new ProductPatchViewModel
        //    {
        //        ProductID = 191919191,
        //        ProductName = string.Empty,
        //        UnitPrice = 1.39m,
        //        UnitsInStock = 90,
        //        UnitsOnOrder = 100
        //    };

        //    IBuildProductsModelsServices productModelsServices = new BuildProductsModelsServices();

        //    customersController.ModelState.AddModelError("ProductName", "Is required");
        //    // Act
        //    var productHttpResponse = customersController.PatchProduct(product);

        //    // Assert
        //    productHttpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        //}

        //[Test]
        //public void Delete_Newly_Created_Product()
        //{
        //    //Arrange
        //    var productName = "Deleted Sause";
        //    var productID = 0;
        //    // We will create the product first then check its there then delete it.
        //    var product = new ProductViewModel
        //    {
        //        ProductName = productName,
        //        UnitPrice = 2,
        //        SupplierID = 1,
        //        CategoryID = 1,
        //        UnitsInStock = 100
        //    };

        //    var productHttpResponse = customersController.PutProduct(product);
        //    var productViewModel = (ProductViewModel)((System.Net.Http.ObjectContent)productHttpResponse.Content).Value;

        //    productID = productViewModel.ProductID;

        //    // Act
        //    var productDeleteHttpResponse = customersController.DeleteProduct(productID);

        //    //Assert
        //    productDeleteHttpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        //}
    }
}
