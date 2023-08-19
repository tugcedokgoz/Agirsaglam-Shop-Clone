using AgirSaglam.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    public class PropertyRepository:RepositoryBase<Property>
    {
        public PropertyRepository(RepositoryContext context):base(context)
        {
            
        }

        public void RemoveProperty(int propertyId)
        {
            RepositoryContext.Properties.Remove(RepositoryContext.Properties.Find(propertyId));
        }

        public Property GetPropertyByName(string propertyName)
        {
            return FindByCondition(property => property.Name == propertyName).FirstOrDefault();
        }


    }
}
