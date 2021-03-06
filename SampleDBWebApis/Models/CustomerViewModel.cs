﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SampleDBWebApis.Models
{
    [ExcludeFromCodeCoverage]

    public class CustomerViewModel
    {
        
        public string CustomerID { get; set; }
        
        [Required]
        public string CompanyName { get; set; }
        
        [Required]
        public string ContactName { get; set; }
        
        public string ContactTitle { get; set; }
        
        [Required]
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
}