using AgirSaglam.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    public class ContactRepository:RepositoryBase<Contact>
    {
        public ContactRepository(RepositoryContext context):base(context)
        {
            
        }
    }
}
