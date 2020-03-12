using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using AutoMapper;
using SampleDBWebApis.ModelBuilders;
using SampleDBWebApis.Models;
using SampleDBWebApis.Service;
using StructureMap;

namespace SampleDBWebApis.Controllers
{
    [RoutePrefix("customers")]
    public class CustomersController : ApiController
    {
        private IBuildCustomersModelServices _buildModelsService;
        private ICustomerModelBuilders _customerModelBuilder;

        [DefaultConstructor]
        public CustomersController(IBuildCustomersModelServices buildModelsService, ICustomerModelBuilders customerModelBuilder)
        {
            _buildModelsService = buildModelsService;
            _customerModelBuilder = customerModelBuilder;
        }

        // GET /api/<controller>/5

        /// <summary>
        /// Returns a Single customer
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSingleCustomer/{id}")]
        public HttpResponseMessage GetSingleCustomer(string Id)
        {
            var CustomerserviceModel = _buildModelsService.GetCustomer(Id);
            CustomerViewModel CustomerViewModel = Mapper.Map<CustomerViewModel>(CustomerserviceModel);
            var returnHttStatusCode = HttpStatusCode.OK;
            
            if (CustomerViewModel == null)
            {
                returnHttStatusCode = HttpStatusCode.NotFound;
            }

            return ReturnResponse(CustomerViewModel, new JsonMediaTypeFormatter(), "application/json", returnHttStatusCode, string.Empty);

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
        public HttpResponseMessage PostCustomer(CustomerViewModel customerModel)
        {

            var custContext = Mapper.Map<DataLayer.Customer>(customerModel);
            custContext = _buildModelsService.CreateNewCustomer(custContext);
            var returnCustomer = Mapper.Map<CustomerViewModel>(custContext);

            return ReturnResponse(returnCustomer, new JsonMediaTypeFormatter(), "application/json", HttpStatusCode.Created, string.Empty);
        }


        // PUT api/<controller>/5
        [HttpPut]
        [Route("PutCustomer")]
        public HttpResponseMessage PutCustomer(CustomerViewModel customerModel)
        {
            if (!ModelState.IsValid)
                return ReturnResponse(new Object(), null, string.Empty, HttpStatusCode.BadRequest, "Not a valid model");

            DataLayer.Customer custContext = _buildModelsService.GetCustomer(customerModel.CustomerID);

            if (custContext != null)
            {
                //custContext = Mapper.Map<DataLayer.Customer>(customerModel);
                //_buildModelsService.UpdateCustomer(custContext);
                //customerModel = Mapper.Map<CustomerViewModel>(custContext);
                customerModel = _customerModelBuilder.BuildPutCustomerModel(customerModel);
                return ReturnResponse(customerModel, new JsonMediaTypeFormatter(), "application/json", HttpStatusCode.OK, string.Empty);
            }
            else
            {
                return ReturnResponse(new Object(), null, string.Empty, HttpStatusCode.NotFound, "Unable to find the Customer");
            }

            return PostCustomer(customerModel);

        }

        [HttpPatch]
        [Route("PatchCustomer")]
        public HttpResponseMessage PatchCustomer(CustomerViewModel CustomerModel)
        {
            return PutCustomer(CustomerModel);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage DeleteCustomer(string Id)
        {
            var CustomerserviceModel = _buildModelsService.GetCustomer(Id);

            if (CustomerserviceModel != null)
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