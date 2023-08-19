using AgirSaglam.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    public class PropertyGroupRepository:RepositoryBase<PropertyGroup>
    {
        public PropertyGroupRepository(RepositoryContext context):base(context)
        {
            
        }

        //silme
        public void RemovePropertyGroup(int groupId)
        {
            RepositoryContext.PropertyGroups.Where(r => r.Id == groupId).ExecuteDelete();
        }

        // Name'e göre arama
        public List<PropertyGroup> GetPropertyGroupByName(string name)
        {
            var group = RepositoryContext.PropertyGroups
                .Where(r => r.Name.Contains(name))
                .ToList();

            return group;
        }
    }
}
