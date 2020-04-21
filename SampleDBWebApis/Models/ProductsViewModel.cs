using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace SampleDBWebApis.Models
{
    [ExcludeFromCodeCoverage]
    public class ProductsViewModel
    {
        public List<ProductViewModel> Products { get; set; }
    }
}