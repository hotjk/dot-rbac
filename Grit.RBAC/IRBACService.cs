using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.RBAC
{
    public interface IRBACService
    {
        IEnumerable<Permission> GetPermissions();
        IEnumerable<Role> GetRoles();
        IEnumerable<Subject> GetSubjects();
        IEnumerable<Role> GetRolesWithPermission();

        Permission GetPermission(int id);
        Role GetRole(int id);
        Subject GetSubject(int id);

        bool SavePermission(Permission permission);
        bool SaveRole(Role role);
        bool SaveSubject(Subject subject);

        void SaveRolePermissions(Role role);
        void SaveSubjectRoles(Subject subject);
    }
}
