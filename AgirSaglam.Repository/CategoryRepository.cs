using AgirSaglam.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    public class CategoryRepository : RepositoryBase<Category>
    {
        public CategoryRepository(RepositoryContext context) : base(context)
        {

        }
        //include denildiği için bir üst kategori getiriyo

        public void RemoveCategory(int categoryId)
        {
            RepositoryContext.Categories.Where(r => r.Id == categoryId).ExecuteDelete();
        }

        // categoryId'ye göre PropertyGroup listesini getirme
        public List<PropertyGroup> GetPropertyGroupsByCategoryId(int categoryId)
        {
            return RepositoryContext.CategoryPropertyGroups
                .Where(cpg => cpg.CategoryId == categoryId)
                .Join(RepositoryContext.PropertyGroups,
                    cpg => cpg.GroupId,
                    pg => pg.Id,
                    (cpg, pg) => pg)
                .ToList();
        }



    }
}