using AgirSaglam.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    public class RoleRepository:RepositoryBase<Role>
    {
        public RoleRepository(RepositoryContext context):base(context)
        {
            
        }
        public void RemoveRole(int roleId)
        {
            RepositoryContext.Roles.Where(r => r.Id == roleId).ExecuteDelete();
        }
        //roleid göre user getirme
        public async Task<List<User>> GetUsersByRoleId(int roleId)
        {
            var users = await RepositoryContext.Users
                .Include(u => u.Adress) // Adress nesnesini Include ederek ilişkili veriyi getirin
                .Where(u => u.RoleId == roleId)
                .ToListAsync();

            return users;
        }

        public List<Role> GetRolesByName(string name)
        {
            var roles = RepositoryContext.Roles
                .Where(r => r.Name.Contains(name))
                .ToList();

            return roles;
        }
    }
}
