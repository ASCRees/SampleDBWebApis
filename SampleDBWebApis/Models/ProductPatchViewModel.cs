using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace SampleDBWebApis.Models
{
    [ExcludeFromCodeCoverage]
    public class ProductPatchViewModel
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please add a product name.")]
        public string ProductName { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<short> UnitsInStock { get; set; }
        public Nullable<short> UnitsOnOrder { get; set; }
        public Nullable<short> ReorderLevel { get; set; }
    }
}