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
        public PropertyGroup GetPropertyGroupByName(string groupName)
        {
            return RepositoryContext.PropertyGroups.FirstOrDefault(p => p.Name == groupName);
        }
    }
}
