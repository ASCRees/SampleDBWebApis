using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using AutoMapper;
using SampleDBWebApis.Models;
using SampleDBWebApis.Service;
using StructureMap;

namespace SampleDBWebApis.Controllers
{
    [RoutePrefix("customers")]
    public class CustomersController : ApiController
    {
        private IBuildCustomersModelServices _buildModelsService;

        [DefaultConstructor]
        public CustomersController(IBuildCustomersModelServices buildModelsService)
        {
            _buildModelsService = buildModelsService;
        }

        // GET /api/<controller>/5

        /// <summary>
        /// Returns a Single customer
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSingleCustomer/{id}")]
        public CustomerViewModel GetSingleCustomer(string Id)
        {
            var CustomerserviceModel = _buildModelsService.GetCustomer(Id);
            CustomerViewModel CustomerViewModel = Mapper.Map<CustomerViewModel>(CustomerserviceModel);

            return CustomerViewModel;
        }

        /// <summary>
        /// Returns multiple Customers 
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMultipleCustomers/{stringValue}")]
        public CustomersViewModel GetMultipleCustomers(string stringValue)
        {
            var CustomersServiceModel = _buildModelsService.GetListOfCustomers(stringValue);
            var Customers = Mapper.Map<List<CustomerViewModel>>(CustomersServiceModel);
            CustomersViewModel CustomersViewModel = new CustomersViewModel();
            CustomersViewModel.Customers = Customers;
            return CustomersViewModel;
        }

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        [HttpPost]
        [Route("PostCustomer")]
        public HttpResponseMessage PostCustomer(CustomerViewModel CustomerModel)
        {

            var prodContext = Mapper.Map<DataLayer.Customer>(CustomerModel);
            prodContext = _buildModelsService.CreateNewCustomer(prodContext);
            var returnCustomer = Mapper.Map<CustomerViewModel>(prodContext);

            return ReturnResponse(returnCustomer, new JsonMediaTypeFormatter(), "application/json", HttpStatusCode.Created, string.Empty);
        }


        // PUT api/<controller>/5
        [HttpPut]
        [Route("PutCustomer")]
        public HttpResponseMessage PutCustomer(CustomerViewModel CustomerModel)
        {
            if (!ModelState.IsValid)
                return ReturnResponse(new Object(), null, string.Empty, HttpStatusCode.BadRequest, "Not a valid model" ); 

            var prodContext = new DataLayer.Customer();

            if (!string.IsNullOrWhiteSpace(CustomerModel.CustomerID))
            {
                prodContext = _buildModelsService.GetCustomer(CustomerModel.CustomerID);

                if (prodContext != null)
                {
                    prodContext=Mapper.Map<DataLayer.Customer>(CustomerModel);
                    _buildModelsService.UpdateCustomer();
                    CustomerModel = Mapper.Map<CustomerViewModel>(prodContext);
                    return ReturnResponse(CustomerModel, new JsonMediaTypeFormatter(), "application/json", HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return ReturnResponse(new Object(), null, string.Empty, HttpStatusCode.NotFound, "Unable to find the Customer");
                }
            }

            return PostCustomer(CustomerModel);

        }

        //[HttpPatch]
        //[Route("PatchCustomer")]
        //public HttpResponseMessage PatchCustomer(CustomerPatchViewModel CustomerModel)
        //{
        //    if (!ModelState.IsValid)
        //        return ReturnResponse(new Object(), null, string.Empty, HttpStatusCode.BadRequest, "Not a valid model");


        //    if (CustomerModel.CustomerID > 0)
        //    {
        //        var prodContext = _buildModelsService.GetCustomer(CustomerModel.CustomerID);

        //        if (prodContext != null)
        //        {

        //            prodContext.CustomerName = CustomerModel.CustomerName;
        //            prodContext.ReorderLevel = CustomerModel.ReorderLevel;
        //            prodContext.UnitPrice = CustomerModel.UnitPrice;
        //            prodContext.UnitsInStock = CustomerModel.UnitsInStock;
        //            prodContext.UnitsOnOrder = CustomerModel.UnitsOnOrder;

        //            _buildModelsService.UpdateCustomer();
        //            CustomerModel = Mapper.Map<CustomerPatchViewModel>(prodContext);
        //            return ReturnResponse(CustomerModel, new JsonMediaTypeFormatter(), "application/json", HttpStatusCode.OK, string.Empty);
        //        }
        //    }
        //    return ReturnResponse(CustomerModel, new JsonMediaTypeFormatter(), "application/json", HttpStatusCode.NotFound, "Unable to find the Customer");
        //}
        // DELETE api/<controller>/5
        public HttpResponseMessage DeleteCustomer(string Id)
        {
            var CustomerserviceModel = _buildModelsService.GetCustomer(Id);

            if (CustomerserviceModel!=null)
            {
                _buildModelsService.DeleteCustomer(Id);
                return ReturnResponse(CustomerserviceModel, new JsonMediaTypeFormatter(), "application/json", HttpStatusCode.NoContent, string.Empty);
            }
            return ReturnResponse(CustomerserviceModel, new JsonMediaTypeFormatter(), "application/json", HttpStatusCode.NotFound, "Unable to find the Customer");


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