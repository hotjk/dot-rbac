using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.RBAC
{
    public interface IRBACService
    {
        Permission GetPermission(int id);
        IEnumerable<Permission> GetPermissions();

        Role GetRole(int id, bool withPermission = true);
        IEnumerable<Role> GetRoles(bool withPermission = false);

        Subject GetSubject(int id, bool withRole = true, bool withRolePermission = true, bool withPermission = true);
        IEnumerable<Subject> GetSubjects();
    }
}
