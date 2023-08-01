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
    }
}