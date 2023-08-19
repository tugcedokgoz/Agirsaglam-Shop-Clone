using AgirSaglam.Model.Models;
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
        public void RemoveProperty(int propertyId)
        {
            var propertyToRemove = RepositoryContext.Properties.Find(propertyId);
            if (propertyToRemove != null)
            {
                RepositoryContext.Properties.Remove(propertyToRemove);
            }
        }
        public IQueryable<ProductProperty> GetByProductId(int productId)
        {
            return FindByCondition(pp => pp.ProductId == productId);
        }

        public IQueryable<ProductProperty> GetByPropertyId(int propertyId)
        {
            return FindByCondition(pp => pp.PropertyId == propertyId);
        }
    }
}
