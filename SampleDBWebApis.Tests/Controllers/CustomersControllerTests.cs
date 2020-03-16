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
using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public void PostCustomer_Create_New_Customer_Mock()
        {
            // Arrange

            var newCustomerModel = Mapper.Map<CustomerViewModel>(createdCustomerModel);

            CustomersController customersController = null;

            // Mock the GetCustomer

            var mockcustomerModelsServices = new Mock<IBuildCustomersModelServices>();
            mockcustomerModelsServices.Setup(x => x.CreateNewCustomer(It.IsAny<DataLayer.Customer>())).Returns(()=> { 
                createdCustomerModel.CustomerID = customerID; 
                return createdCustomerModel; 
            });

            customersController = new CustomersController(mockcustomerModelsServices.Object, new CustomerModelBuilders(mockcustomerModelsServices.Object));


            // Act
            var customerHttpResponse = customersController.PostCustomer(newCustomerModel);
            var customerResultsViewModel = (CustomerViewModel)((System.Net.Http.ObjectContent)customerHttpResponse.Content).Value;

            // Assert
            customerResultsViewModel.CustomerID.Should().Be(customerID);

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
        public void PutCustomer_Valid_Update_Existing_Customer_Mock()
        {
            // Arrange

            var customerID = "ALFKI";

            var getSingleCustomerResponse = customersController.GetSingleCustomer(customerID);
            CustomerViewModel customerViewModel = (CustomerViewModel)((System.Net.Http.ObjectContent)getSingleCustomerResponse.Content).Value;
 
            var companyName = "Company Update " + DateTime.Now.ToString();

            customerViewModel.CompanyName = companyName;

            var custContext = Mapper.Map<DataLayer.Customer>(customerViewModel);

            // Mock the GetCustomer

            var mockcustomerModelsServices = new Mock<IBuildCustomersModelServices>();
            mockcustomerModelsServices.Setup(x => x.GetCustomer(It.IsAny<string>())).Returns(custContext);
            mockcustomerModelsServices.Setup(x => x.UpdateCustomer()).Returns(1);

            customersController = new CustomersController(mockcustomerModelsServices.Object, new CustomerModelBuilders(mockcustomerModelsServices.Object));

            // Act
            var customerHttpResponse = customersController.PutCustomer(customerViewModel);
            var customerResultViewModel = (CustomerViewModel)((System.Net.Http.ObjectContent)customerHttpResponse.Content).Value;

            // Assert
            customerResultViewModel.CompanyName.Should().BeEquivalentTo(companyName);
            customerHttpResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
        }

        [Test]
        public void PutCustomer_Valid_Update_Existing_Customer_Throws_Error_Mock()
        {
            // Arrange

            var newCustomerModel = Mapper.Map<CustomerViewModel>(createdCustomerModel);

            // Mock the GetCustomer

            var mockcustomerModelsServices = new Mock<IBuildCustomersModelServices>();
            DataLayer.Customer returnCustomerVal = null;
            mockcustomerModelsServices.Setup(x => x.CreateNewCustomer(It.IsAny<DataLayer.Customer>())).Throws(new ArgumentException("Invalid Value"));
            mockcustomerModelsServices.Setup(x => x.GetCustomer(It.IsAny<string>())).Returns(returnCustomerVal);
            customersController = new CustomersController(mockcustomerModelsServices.Object, new CustomerModelBuilders(mockcustomerModelsServices.Object));

            // Act
            var customerHttpResponse = customersController.PutCustomer(newCustomerModel);

            // Assert
            customerHttpResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.BadRequest);


        }

        [Test]
        public void PutCustomer_Valid_Update_New_Customer_Mock()
        {
            // Arrange

            var newCustomerModel = Mapper.Map<CustomerViewModel>(createdCustomerModel);

            // Mock the GetCustomer

            var mockcustomerModelsServices = new Mock<IBuildCustomersModelServices>();
            DataLayer.Customer returnCustomerVal = null;
            mockcustomerModelsServices.Setup(x => x.CreateNewCustomer(It.IsAny<DataLayer.Customer>())).Returns(() => {
                createdCustomerModel.CustomerID = customerID;
                return createdCustomerModel;
            });
            mockcustomerModelsServices.Setup(x => x.GetCustomer(It.IsAny<string>())).Returns(returnCustomerVal);
            customersController = new CustomersController(mockcustomerModelsServices.Object, new CustomerModelBuilders(mockcustomerModelsServices.Object));

            // Act
            var customerHttpResponse = customersController.PutCustomer(newCustomerModel);
            var customerResultsViewModel = (CustomerViewModel)((System.Net.Http.ObjectContent)customerHttpResponse.Content).Value;

            // Assert
            customerResultsViewModel.CustomerID.Should().Be(customerID);
        }

        [Test]
        public void PutCustomer_Customer_Invalid_Model()
        {
            // Arrange

            var updateCustomerModel = new CustomerViewModel
            {
                CustomerID = "TEST1",
                CompanyName = null,
                ContactName = "Gus",
                Phone = "555-2121",
                City = "Alburquerque",
                ContactTitle = "Don",
                Address = "1 Main Street",
                PostalCode = "76243",
                Region = "",
                Country = "USA"
            };

            var result = new List<ValidationResult>();

            // Act​
            var isValid = Validator.TryValidateObject(updateCustomerModel, new System.ComponentModel.DataAnnotations.ValidationContext(updateCustomerModel), result);
            
            // Asset
            isValid.Should().BeFalse();

        }

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
