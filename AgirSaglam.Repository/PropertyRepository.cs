using AgirSaglam.Model.Models;
using Microsoft.EntityFrameworkCore;
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
            RepositoryContext.Properties.Where(r => r.Id == propertyId).ExecuteDelete();
        }

        public List<Property> GetPropertyByName(string name)
        {
            var property = RepositoryContext.Properties
                .Where(r => r.Name.Contains(name))
                .ToList();

            return property;
        }

    }
}
