using AgirSaglam.Model;
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
       
    }
}
