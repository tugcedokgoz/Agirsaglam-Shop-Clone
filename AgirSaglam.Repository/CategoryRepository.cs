using AgirSaglam.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    public class CategoryRepository:RepositoryBase<Category>
    {
        public CategoryRepository(RepositoryContext context):base(context)
        {
            
        }
        //include denildiği için bir üst kategori getiriyo

        public void RemoveCategory(int categoryId)
        {
            RepositoryContext.Categories.Where(r => r.Id == categoryId).ExecuteDelete();
        }
    }
}
