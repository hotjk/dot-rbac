using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.RBAC
{
    public class RBACWriteService : IRBACWriteService
    {
        private IRBACWriteRepository RBACWriteRepository { get; set; }

        public RBACWriteService(IRBACWriteRepository RBACWriteRepository)
        {
            this.RBACWriteRepository = RBACWriteRepository;
        }

        public bool SavePermission(Permission permission)
        {
            return RBACWriteRepository.SavePermission(permission);
        }

        public bool SaveRole(Role role)
        {
            return RBACWriteRepository.SaveRole(role);
        }

        public bool SaveSubject(Subject subject)
        {
            return RBACWriteRepository.SaveSubject(subject);
        }

        public void SaveRolePermissions(Role role)
        {
            RBACWriteRepository.SaveRolePermissions(role);
        }

        public void SaveRolePermissions(IEnumerable<Role> roles)
        {
            RBACWriteRepository.SaveRolePermissions(roles);
        }

        public void SaveSubjectRoles(Subject subject)
        {
            RBACWriteRepository.SaveSubjectRoles(subject);
        }

        public void SaveSubjectPermissions(Subject subject)
        {
            RBACWriteRepository.SaveSubjectPermissions(subject);
        }
    }
}
