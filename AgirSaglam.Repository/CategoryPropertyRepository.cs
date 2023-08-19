using AgirSaglam.Model.Models;
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
        public void RemoveCategoryPropertyGroup(int categoryId, int groupId)
        {
            var itemToRemove = RepositoryContext.Set<CategoryPropertyGroup>()
                .FirstOrDefault(cpg => cpg.CategoryId == categoryId && cpg.GroupId == groupId);

            if (itemToRemove != null)
            {
                RepositoryContext.Set<CategoryPropertyGroup>().Remove(itemToRemove);
            }
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
