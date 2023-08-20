using AgirSaglam.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    public class CategoryPropertyRepository : RepositoryBase<CategoryPropertyGroup>
    {
        public CategoryPropertyRepository(RepositoryContext contex) : base(contex)
        {
            
        }
        public void RemoveProperty(int CategoryPropertyId)
        {
            RepositoryContext.CategoryPropertyGroups.Where(r => r.Id == CategoryPropertyId).ExecuteDelete();
        }
        public IQueryable<CategoryPropertyGroup> GetGroupsByCategoryId(int categoryId)
        {
            return RepositoryContext.Set<CategoryPropertyGroup>()
                .Where(cpg => cpg.CategoryId == categoryId);
        }
        public IQueryable<CategoryPropertyGroup> GetGroupsByGroupId(int groupId)
        {
            return RepositoryContext.Set<CategoryPropertyGroup>()
                .Where(cpg => cpg.GroupId == groupId);
        }
    }
}
