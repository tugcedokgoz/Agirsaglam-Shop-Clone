using AgirSaglam.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(RepositoryContext context) : base(context)
        {

        }
        //silme
        public void RemoveUser(int userId)
        {
            RepositoryContext.Users.Where(r => r.Id == userId).ExecuteDelete();
        }
        //userId göre role listeleme
        public async Task<Role> GetUserRoleById(int userId)
        {
            var user = await RepositoryContext.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
                return null;

            var role = await RepositoryContext.Roles
                .Where(r => r.Id == user.RoleId)
                .FirstOrDefaultAsync();

            return role;
        }

        //userId göre adress getirme
        public async Task<Adress> GetUserAdressById(int userId)
        {
            var user = await RepositoryContext.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
                return null;

            var adress = await RepositoryContext.Adresses
                .Where(r => r.Id == user.AdressId)
                .FirstOrDefaultAsync();

            return adress;
        }

    }
}
