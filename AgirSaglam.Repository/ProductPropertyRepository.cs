using AgirSaglam.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    public class ProductPropertyRepository:RepositoryBase<ProductProperty>
    {
        public ProductPropertyRepository(RepositoryContext contex):base(contex)
        {
            
        }

        //silme
        public void RemoveProperty(int ProductPropertyId)
        {
            RepositoryContext.ProductProperties.Where(r => r.Id == ProductPropertyId).ExecuteDelete();
        }

        public IQueryable<ProductProperty> GetByProductId(int productId)
        {
            return FindByCondition(pp => pp.ProductId == productId)
                .Include(pp => pp.Property) 
                .ThenInclude(p => p.Group); 
        }

        public IQueryable<ProductProperty> GetByPropertyId(int propertyId)
        {
            return FindByCondition(pp => pp.PropertyId == propertyId);
        }
    }
}
