using AutoMapper;
using SampleDBWebApis.Models;
using SampleDBWebApis.Services;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleDBWebApis.ModelBuilders
{
    public class ProductModelBuilders : IProductModelBuilders
    {
        private IBuildProductsModelsServices _buildModelsService;

        [DefaultConstructor]
        public ProductModelBuilders(IBuildProductsModelsServices buildModelsService)
        {
            _buildModelsService = buildModelsService;
        }

        public ProductViewModel BuildPutProductModel(ProductViewModel productModel)
        {
            var prodContext = new DataLayer.Product();
            prodContext = _buildModelsService.GetProduct(productModel.ProductID);
            if (prodContext != null)
            {
                prodContext.ProductName = productModel.ProductName;
                prodContext.CategoryID = productModel.CategoryID;
                prodContext.Discontinued = productModel.Discontinued;
                prodContext.QuantityPerUnit = productModel.QuantityPerUnit;
                prodContext.ReorderLevel = productModel.ReorderLevel;
                prodContext.SupplierID = productModel.SupplierID;
                prodContext.UnitPrice = productModel.UnitPrice;
                prodContext.UnitsInStock = productModel.UnitsInStock;
                prodContext.UnitsOnOrder = productModel.UnitsOnOrder;
                _buildModelsService.UpdateProduct();
                return Mapper.Map<ProductViewModel>(prodContext);
            }
            else
            {
                return new ProductViewModel();
            }
        }

        public ProductPatchViewModel BuildPatchProductModel(ProductPatchViewModel productModel)
        {
            var prodContext = _buildModelsService.GetProduct(productModel.ProductID);

            if (prodContext != null)
            {
                prodContext.ProductName = productModel.ProductName;
                prodContext.ReorderLevel = productModel.ReorderLevel;
                prodContext.UnitPrice = productModel.UnitPrice;
                prodContext.UnitsInStock = productModel.UnitsInStock;
                prodContext.UnitsOnOrder = productModel.UnitsOnOrder;

                _buildModelsService.UpdateProduct();
                return Mapper.Map<ProductPatchViewModel>(prodContext);
            }
            else
            {
                return new ProductPatchViewModel();
            }
        }
    }
}