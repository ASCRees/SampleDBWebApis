using AutoMapper;
using SampleDBWebApis.Models;
using SampleDBWebApis.DataLayer;
using System.Diagnostics.CodeAnalysis;

namespace SampleDBWebApis.App_Start
{
    [ExcludeFromCodeCoverage]

    public class AutoMapperConfig : Profile
    {
        /// <summary>
        /// Creates the mappings.
        /// </summary>
        public static void CreateMappings()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<ProductViewModel,Product>();

                cfg.CreateMap <Product, ProductPatchViewModel>();
                cfg.CreateMap<ProductPatchViewModel,Product>();

                cfg.CreateMap<Customer, CustomerViewModel>();
                cfg.CreateMap<CustomerViewModel,Customer>();
            });
        }

    }
}