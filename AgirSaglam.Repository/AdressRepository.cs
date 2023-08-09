using AgirSaglam.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    public class AdressRepository:RepositoryBase<Adress>
    {
        public AdressRepository(RepositoryContext context):base(context)
        {
            
        }
        public void RemoveAdress(int adressId)
        {
            RepositoryContext.Adresses.Where(r => r.Id == adressId).ExecuteDelete();
        }
    }
}
