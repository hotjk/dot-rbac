using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.RBAC
{
    public interface IRBACRepository
    {
        Permission GetPermission(int id);
        IEnumerable<Permission> GetPermissions();

        Role GetRole(int id, bool withPermission);
        IEnumerable<Role> GetRoles(bool withPermission);

        Subject GetSubject(int id, bool withRole, bool withRolePermission, bool withPermission);
        IEnumerable<Subject> GetSubjects();
    }
}
