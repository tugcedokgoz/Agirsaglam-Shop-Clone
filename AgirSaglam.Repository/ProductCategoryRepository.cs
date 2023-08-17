using AgirSaglam.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    public class ProductCategoryRepository : RepositoryBase<ProductCategory>
    {
        public ProductCategoryRepository(RepositoryContext context) : base(context)
        {
         
        }
        public void CreateProductWithCategories(Product newProduct, List<int> selectedCategoryIds)
        {
            // Yeni ürünü eklemek
            RepositoryContext.Products.Add(newProduct);
            RepositoryContext.SaveChanges();

            // Ürün ve kategorileri ilişkilendirmek
            foreach (var categoryId in selectedCategoryIds)
            {
                var productCategory = new ProductCategory
                {
                    ProductId = newProduct.Id, // Oluşturulan ürünün ID'si
                    CategoryId = categoryId // Seçilen kategori ID'si
                };

                RepositoryContext.ProductCategories.Add(productCategory);
            }

            RepositoryContext.SaveChanges();
        }
    }
}
