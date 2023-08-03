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

        //kategori id ye göre
        //public List<Product> GetProductsByCategoryId(int categoryId)
        //{
        //    var productIds = RepositoryContext.ProductCategories
        //        .Where(pc => pc.CategoryId == categoryId)
        //        .Select(pc => pc.ProductId)
        //        .ToList();

        //    var products = RepositoryContext.Products
        //        .Where(p => productIds.Contains(p.Id))
        //        .ToList();

        //    return products;
        //}

        public List<object> GetProductsByCategoryId(int categoryId)
        {
            var products = RepositoryContext.ProductCategories
                .Where(pc => pc.CategoryId == categoryId)
                .Select(pc => new
                {
                    Id = pc.Product.Id,
                    Name = pc.Product.Name,
                    Price = pc.Product.Price,
                    DiscountPrice = pc.Product.DiscountPrice,
                    Amount = pc.Product.Amount,
                    Description = pc.Product.Description,
                    Image = pc.Product.Image,
                    CategoryName = pc.Category.Name
                })
                .ToList();

            return products.Cast<object>().ToList();//tipi bilinmeyen yani categoryname için
        }


        //silme
        public void RemoveProduct(int productId)
        {
            RepositoryContext.Products.Where(r => r.Id == productId).ExecuteDelete();
        }

        // productId'ye göre ürüne ait özellikleri listeleme
        public List<Property> GetPropertiesByProductId(int productId)
        {
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