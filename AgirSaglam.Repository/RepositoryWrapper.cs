using AgirSaglam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    public class RepositoryWrapper
    {
        private RepositoryContext context;

        private CategoryRepository categoryRepository;

        public RepositoryWrapper(RepositoryContext context)
        {
            this.context = context;
        }

        public CategoryRepository CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(context);
                return categoryRepository;
            }
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
