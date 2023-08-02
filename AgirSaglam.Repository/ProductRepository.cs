using AgirSaglam.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(RepositoryContext context) : base(context)
        {

        }
        //kategori id göre ürün listeleme
        public List<Product> ProductGetByCategoryId(int categoryId)
        {
            List<Product> items = (from u in RepositoryContext.Products
                                join k in RepositoryContext.ProductCategories on u.Id equals k.ProductId
                                where k.CategoryId == categoryId
                                select u).ToList<Product>();
            return items;
        }

        //silme
        public void RemoveProduct(int productId)
        {
            RepositoryContext.Products.Where(r => r.Id == productId).ExecuteDelete();
        }

        // productId'ye göre ürüne ait özellikleri listeleme
        public List<Property> GetPropertiesByProductId(int productId)
        {
            var product = RepositoryContext.Products
                .Where(p => p.Id == productId)
                .FirstOrDefault();

            if (product == null)
                return new List<Property>();

            var propertyIds = RepositoryContext.ProductProperties
                .Where(pp => pp.ProductId == productId)
                .Select(pp => pp.PropertyId)
                .ToList();

            var properties = RepositoryContext.Properties
                .Where(p => propertyIds.Contains(p.Id))
                .ToList();

            return properties;
        }
    }
}