using AutoMapper;
using SampleDBWebApis.ModelBuilders;
using SampleDBWebApis.Models;
using SampleDBWebApis.Services;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace SampleDBWebApis.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private IBuildProductsModelsServices _buildModelsService;
        private IProductModelBuilders _productModelBuilder;

        [DefaultConstructor]
        public ProductsController(IBuildProductsModelsServices buildModelsService, IProductModelBuilders productModelBuilder)
        {
            _buildModelsService = buildModelsService;
            _productModelBuilder = productModelBuilder;
        }

        // GET /api/<controller>/5

        /// <summary>
        /// Returns a Single product
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSingleProduct/{id}")]
        public ProductViewModel GetSingleProduct(Int32 Id)
        {
            var productServiceModel = _buildModelsService.GetProduct(Id);
            ProductViewModel productViewModel = Mapper.Map<ProductViewModel>(productServiceModel);

            return productViewModel;
        }

        /// <summary>
        /// Returns multiple products
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMultipleProducts/{stringValue}")]
        public ProductsViewModel GetMultipleProducts(string stringValue)
        {
            var productsServiceModel = _buildModelsService.GetListOfProducts(stringValue);
            var products = Mapper.Map<List<ProductViewModel>>(productsServiceModel);
            ProductsViewModel productsViewModel = new ProductsViewModel();
            productsViewModel.Products = products;
            return productsViewModel;
        }

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        [HttpPost]
        [Route("PostProduct")]
        public HttpResponseMessage PostProduct(ProductViewModel productModel)
        {
            var prodContext = Mapper.Map<DataLayer.Product>(productModel);
            prodContext = _buildModelsService.CreateNewProduct(prodContext);
            var returnProduct = Mapper.Map<ProductViewModel>(prodContext);

            return ReturnResponse(returnProduct, new JsonMediaTypeFormatter(), "application/json", HttpStatusCode.Created, string.Empty);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("PutProduct")]
        public HttpResponseMessage PutProduct(ProductViewModel productModel)
        {
            if (!ModelState.IsValid)
                return ReturnResponse(new Object(), null, string.Empty, HttpStatusCode.BadRequest, "Not a valid model");

            if (productModel.ProductID > 0)
            {
                productModel = _productModelBuilder.BuildPutProductModel(productModel);

                if (productModel.ProductID > 0)
                {
                    return ReturnResponse(productModel, new JsonMediaTypeFormatter(), "application/json", HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return ReturnResponse(new Object(), null, string.Empty, HttpStatusCode.NotFound, "Unable to find the product");
                }
            }

            return PostProduct(productModel);
        }

        [HttpPatch]
        [Route("PatchProduct")]
        public HttpResponseMessage PatchProduct(ProductPatchViewModel productModel)
        {
            if (!ModelState.IsValid)
                return ReturnResponse(new Object(), null, string.Empty, HttpStatusCode.BadRequest, "Not a valid model");

            if (productModel.ProductID > 0)
            {
                productModel = _productModelBuilder.BuildPatchProductModel(productModel);

                if (productModel.ProductID > 0)
                {
                    return ReturnResponse(productModel, new JsonMediaTypeFormatter(), "application/json", HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return ReturnResponse(new Object(), null, string.Empty, HttpStatusCode.NotFound, "Unable to find the product");
                }
            }
            return ReturnResponse(productModel, new JsonMediaTypeFormatter(), "application/json", HttpStatusCode.NotFound, "Unable to find the product");
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage DeleteProduct(int Id)
        {
            var productServiceModel = _buildModelsService.GetProduct(Id);

            if (productServiceModel != null)
            {
                _buildModelsService.DeleteProduct(Id);
                return ReturnResponse(productServiceModel, new JsonMediaTypeFormatter(), "application/json", HttpStatusCode.NoContent, string.Empty);
            }
            return ReturnResponse(productServiceModel, new JsonMediaTypeFormatter(), "application/json", HttpStatusCode.NotFound, "Unable to find the product");
        }

        private HttpResponseMessage ReturnResponse<T>(T returnObject, MediaTypeFormatter formatter, string formatString, HttpStatusCode statusCode, string returnPhrase)
        {
            //var content =

            var response = new HttpResponseMessage(statusCode)
            {
                Content = returnObject.GetType() == new Object().GetType() ? null : new ObjectContent<T>(returnObject, formatter, formatString),
                ReasonPhrase = returnPhrase
            };

            return response;
        }
    }
}