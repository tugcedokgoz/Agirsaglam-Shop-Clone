using AgirSaglam.Model.Models;
using AgirSaglam.Model.View;
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

        public List<Product> GetProductsByName(string name)
        {
            var products = RepositoryContext.Products
                .Where(r => r.Name.Contains(name))
                .ToList();

            return products;
        }

        public List<V_ProductAdminList> GetProductAdminList()
        {
            var products = RepositoryContext.ProductAdminList
                .Select(p => new V_ProductAdminList
                {
                    ProductId = p.ProductId,
                    CategoryId = p.CategoryId,
                    ProductName = p.ProductName,
                    CategoryName = p.CategoryName,
                    Price = p.Price,
                    DiscountPrice = p.DiscountPrice,
                    Amount = p.Amount,
                    Description = p.Description,
                    Image = p.Image
                })
                .ToList();

            return products;
        }
        public Product GetProductById(int productId)
        {
            return RepositoryContext.Products
                .Include(p => p.Categories)
                .Include(p => p.ProductCategories)
                .FirstOrDefault(p => p.Id == productId);
        }

        //parentcategory id ye göre childlardaki tüm productları getirme
        public List<object> GetProductsByParentCategoryId(int parentCategoryId)
        {
            var categoriesUnderParent = RepositoryContext.Categories
                .Where(c => c.ParentCategoryId == parentCategoryId)
                .Select(c => c.Id)
                .ToList();

            var products = RepositoryContext.ProductCategories
                .Where(pc => categoriesUnderParent.Contains(pc.CategoryId.Value))
                .Select(pc => new
                {
                    Id = pc.Product.Id,
                    Name = pc.Product.Name,
                    Price = pc.Product.Price,
                    DiscountPrice = pc.Product.DiscountPrice,
                    Amount = pc.Product.Amount,
                    Description = pc.Product.Description,
                    Image = pc.Product.Image,
                    CategoryNames = pc.Product.Categories.Select(c => c.Name).ToList()  // Child category names
                })
                .ToList();

            return products.Cast<object>().ToList();
        }

        // DiscountPrice null veya 0 değilse
        public List<Product> GetDiscountedProduct()
        {
            var products = RepositoryContext.Products
                .Where(p => p.DiscountPrice != null && p.DiscountPrice > 0) 
                .ToList();

            return products;
        }


    }
}