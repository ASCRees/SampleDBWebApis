namespace SampleDBWebApis.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using SampleDBWebApis.DataLayer;

    public class BuildProductsModelsServices : DbContext, IBuildProductsModelsServices
    {
        
        SampleDBEntities _context;

        public SampleDBEntities Context
        { 
            get {
                    if (_context == null)
                    {
                        _context = new SampleDBEntities();
                    }
                    return _context;
                }  
        }

        public Product CreateNewProduct(Product prodContext)
        {
            Context.Products.Add(prodContext);
            Context.SaveChanges();
            return prodContext;
        }

        public List<Product> GetListOfProducts(string productSearch)
        {
            return Context.Products
                                   .Where(s => s.ProductName.StartsWith(productSearch))
                                   .ToList();
        }

        public Product GetProduct(int Id)
        {
                return Context.Products
                                   .Where(s => s.ProductID == Id)
                                   .FirstOrDefault<Product>();
        }

        public int UpdateProduct()
        {
            return Context.SaveChanges();
        }

        public int DeleteProduct(Int32 ProductID)
        {
            Product prod = GetProduct(ProductID);
            Context.Products.Attach(prod);
            Context.Products.Remove(prod);
            return Context.SaveChanges();
        }
       
    }
}