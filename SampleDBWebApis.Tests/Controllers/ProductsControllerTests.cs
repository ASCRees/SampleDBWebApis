using System.Net;
using FluentAssertions;
using SampleDBWebApis.Services;
using NUnit.Framework;
using SampleDBWebApis.Controllers;
using SampleDBWebApis.Models;
using SampleDBWebApis.App_Start;
using SampleDBWebApis.Tests.Base;
using SampleDBWebApis.ModelBuilders;

namespace SampleDBWebApis.Tests.Controllers
{
    public class ProductsControllerTests:BaseProductTests
    {
        [SetUp]
        public override void Setup()
        {
            AutoMapperConfig.CreateMappings();
            base.Setup();
        }

        [Test]
        public void Get_Product_By_ID()
        {
            // Arrange
            var productID = 1;

            // Act
            ProductViewModel productViewModel = productsController.GetSingleProduct(productID);

            // Assert
            productViewModel.ProductID.Should().Be(productID);

        }

        [Test]
        public void Get_Products_By_Starting_String()
        {
            // Arrange
            var prodString = "M";

            // Act
            ProductsViewModel productViewModel = productsController.GetMultipleProducts(prodString);

            // Assert
            productViewModel.Products.Count.Should().BeGreaterThan(0);

        }

        [Test]
        public void PostProduct_Create_New_Product()
        {
            // Arrange
            ProductViewModel product = new ProductViewModel
            {
                ProductName = "Tomatoe Ketchup",
                UnitPrice = 1.59m,
                UnitsInStock = 200,
                SupplierID = 1,
                CategoryID = 1,
                UnitsOnOrder = 0
            };

            // Act
            var productHttpResponse = productsController.PostProduct(product);
            var productViewModel =(ProductViewModel)((System.Net.Http.ObjectContent)productHttpResponse.Content).Value;

            // Assert
            productViewModel.ProductID.Should().BeGreaterThan(0);

        }

        [Test]
        public void PutProduct_Update_Product()
        {
            // Arrange
            ProductViewModel product = new ProductViewModel
            {
                ProductID=84,
                ProductName = "BBQ Sauce",
                UnitPrice = 1.39m,
                UnitsInStock = 90,
                SupplierID = 1,
                CategoryID = 1,
                UnitsOnOrder = 100,

            };

            // Act
            var productHttpResponse = productsController.PutProduct(product);
       
            var productViewModel = (ProductViewModel)((System.Net.Http.ObjectContent)productHttpResponse.Content).Value;

            // Assert
            productViewModel.UnitsInStock.Should().Be(90);

        }

        [Test]
        public void PutProduct_Update_Product_NotFound()
        {
            // Arrange
            ProductViewModel product = new ProductViewModel
            {
                ProductID = 191919191,
                ProductName = "BBQ Sauce",
                UnitPrice = 1.39m,
                UnitsInStock = 90,
                SupplierID = 1,
                CategoryID = 1,
                UnitsOnOrder = 100,

            };

            // Act
            var productHttpResponse = productsController.PutProduct(product);

            // Assert
            productHttpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }

        [Test]
        public void PutProduct_Update_Product_Model_Not_Valid()
        {
            // Arrange
            ProductViewModel product = new ProductViewModel
            {
                ProductID = 191919191,
                ProductName = string.Empty,
                UnitPrice = 1.39m,
                UnitsInStock = 90,
                SupplierID = 1,
                CategoryID = 1,
                UnitsOnOrder = 100,

            };

            productsController.ModelState.AddModelError("ProductName", "Is required");
            // Act
            var productHttpResponse = productsController.PutProduct(product);

            // Assert
            productHttpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

        [Test]
        public void PutProduct_Create_Product()
        {
            // Arrange
            ProductViewModel product = new ProductViewModel
            {
                ProductName = "Salad Cream",
                UnitPrice = 2.39m,
                UnitsInStock = 190,
                SupplierID = 1,
                CategoryID = 1,
                UnitsOnOrder = 50,
                QuantityPerUnit="10",
                ReorderLevel=20,
                Discontinued=false

            };

            // Act
            var productHttpResponse = productsController.PutProduct(product);

            // Assert
            productHttpResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        }

        [Test]
        public void PatchProduct_Update_Product()
        {
            // Arrange
            ProductPatchViewModel product = new ProductPatchViewModel
            {
                ProductID = 84,
                ProductName = "BullsEye BBQ Sauce",
                UnitPrice = 1.19m,
                UnitsInStock = 91,
                UnitsOnOrder = 11,
                ReorderLevel=5

            };


            // Act
            var productHttpResponse = productsController.PatchProduct(product);

            var productViewModel = (ProductPatchViewModel)((System.Net.Http.ObjectContent)productHttpResponse.Content).Value;

            // Assert
            productViewModel.UnitsInStock.Should().Be(91);

        }

        [Test]
        public void PatchProduct_Update_Product_Not_Found()
        {
            // Arrange
            ProductPatchViewModel product = new ProductPatchViewModel
            {
                ProductID = 19191919,
                ProductName = "BullsEye BBQ Sauce",
                UnitPrice = 1.19m,
                UnitsInStock = 91,
                UnitsOnOrder = 11,
                ReorderLevel = 5

            };


            // Act
            var productHttpResponse = productsController.PatchProduct(product);

            var productViewModel = (ProductPatchViewModel)((System.Net.Http.ObjectContent)productHttpResponse.Content).Value;

            // Assert
            productHttpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }

        [Test]
        public void PatchProduct_Update_Product_Model_Not_Valid()
        {
            // Arrange
            ProductPatchViewModel product = new ProductPatchViewModel
            {
                ProductID = 191919191,
                ProductName = string.Empty,
                UnitPrice = 1.39m,
                UnitsInStock = 90,
                UnitsOnOrder = 100
            };

            IBuildProductsModelsServices productModelsServices = new BuildProductsModelsServices();

            productsController.ModelState.AddModelError("ProductName", "Is required");
            // Act
            var productHttpResponse = productsController.PatchProduct(product);

            // Assert
            productHttpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

        [Test]
        public void Delete_Newly_Created_Product()
        {
            //Arrange
            var productName = "Deleted Sause";
            var productID = 0;
            // We will create the product first then check its there then delete it.
            var product = new ProductViewModel
            {
                ProductName = productName,
                UnitPrice = 2,
                SupplierID = 1,
                CategoryID = 1,
                UnitsInStock = 100
            };

            var productHttpResponse = productsController.PutProduct(product);
            var productViewModel = (ProductViewModel)((System.Net.Http.ObjectContent)productHttpResponse.Content).Value;

            productID = productViewModel.ProductID;

            // Act
            var productDeleteHttpResponse = productsController.DeleteProduct(productID);

            //Assert
            productDeleteHttpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
