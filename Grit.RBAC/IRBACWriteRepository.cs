using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.RBAC
{
    public interface IRBACWriteRepository
    {
        bool SavePermission(Permission permission);
        
        bool SaveRole(Role role);
        void SaveRolePermissions(Role role);
        void SaveRolePermissions(IEnumerable<Role> roles);

        bool SaveSubject(Subject subject);
        void SaveSubjectRoles(Subject subject);
        void SaveSubjectPermissions(Subject subject);
    }
}
