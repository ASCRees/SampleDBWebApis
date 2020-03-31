using FluentAssertions;
using SampleDBWebApis.Services;
using NUnit.Framework;
using SampleDBWebApis.DataLayer;
using System;
using SampleDBWebApis.Tests.Base;
using Moq;
using SampleDBWebApis.Service;

namespace SampleDBWebApis.Tests.ServiceLayer
{
    [TestFixture]
    public class CustomerTests : BaseCustomerTests
    {

        [SetUp]
        public override void Setup()
        {
            base.Setup();
        }

        [Test]
        public void Select_A_Single_CustomerID()
        {
            //Arrange
            var customerId = "ANATR";

            //Act
            var customer = customerModelsServices.GetCustomer(customerId);

            //Assert
            customer.CustomerID.Should().Be(customerId);
        }

        [Test]
        public void Select_A_Single_InvalidCustomerID()
        {
            //Arrange
            var customerId = "ANAT";

            //Act
            var customer = customerModelsServices.GetCustomer(customerId);

            //Assert
            customer.Should().BeNull(because : "Customer is found");
        }

        [Test]
        public void Select_Multiple_Products()
        {
            //Arrange
            var startsWith = "M";

            //Act
            var customers = customerModelsServices.GetListOfCustomers(startsWith);

            //Assert
            customers.Count.Should().Be(4);
        }

        [Test]
        public void Update_Customer_Name()
        {
            //Arrange
            Mock<IBuildCustomersModelServices> mockCustomer = new Mock<IBuildCustomersModelServices>();
            mockCustomer.Setup(x => x.GetCustomer(It.IsAny<string>())).Returns(new Customer
            {
                CustomerID = "JSATZ",
                CompanyName = "John Smith PLC",
                ContactName = "John Smith",
                ContactTitle = "CEO",
                Address = "1 High Street",
                City = "NewTown",
                PostalCode = "NE13DR",
                Country = "UK"
            });
            mockCustomer.Setup(x => x.Context.SaveChanges()).Returns(1);
            
            customerModelsServices= mockCustomer.Object;

            var customerId = "JSATZ";
            var customer = customerModelsServices.GetCustomer(customerId);

            //Act
            customer.ContactName = "Ana Smith";


            //Assert
            customerModelsServices.Context.SaveChanges().Should().Be(1);
        }



    }
}