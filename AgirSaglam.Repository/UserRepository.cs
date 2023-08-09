using AgirSaglam.Model.Models;
using AgirSaglam.Model.View;
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
        //tüm kullanıcıları listeleme
        public async Task<(Role Role, Adress Adress)> GetUserRoleAndAdressById(int userId)
        {
            var user = await RepositoryContext.Users
                .Include(u => u.Role) // Role nesnesini Include ederek ilişkili veriyi getirin
                .Include(u => u.Adress) // Adress nesnesini Include ederek ilişkili veriyi getirin
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
                return (null, null);

            return (user.Role, user.Adress);
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

        //userId göre listeleme
        public async Task<User> GetUserById(int userId)
        {
            var user = await RepositoryContext.Users
                .Include(u => u.Role) // Role nesnesini Include ederek ilişkili veriyi getirin
                .Include(u => u.Adress) // Adress nesnesini Include ederek ilişkili veriyi getirin
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            return user;
        }

        //Aktif kullaniciları getirme
        public List<V_AktiveUsers> GetAktiveUsers()
        {
            return RepositoryContext.AktiveUsers.ToList<V_AktiveUsers>();
        }


    }
}
