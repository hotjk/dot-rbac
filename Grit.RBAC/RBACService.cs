using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.RBAC
{
    public class RBACService : IRBACService
    {
        private IRBACRepository RBACRepository { get; set; }

        public RBACService(IRBACRepository RBACRepository)
        {
            this.RBACRepository = RBACRepository;
        }

        public IEnumerable<Permission> GetPermissions()
        {
            return RBACRepository.GetPermissions();
        }

        public IEnumerable<Role> GetRoles(bool withPermission)
        {
            return RBACRepository.GetRoles(withPermission);
        }

        public IEnumerable<Subject> GetSubjects()
        {
            return RBACRepository.GetSubjects();
        }

        public Permission GetPermission(int id)
        {
            return RBACRepository.GetPermission(id);
        }

        public Role GetRole(int id, bool withPermission)
        {
            return RBACRepository.GetRole(id, withPermission);
        }

        public Subject GetSubject(int id, bool withRole, bool withRolePermission, bool withPermission)
        {
            return RBACRepository.GetSubject(id, withRole, withRolePermission, withPermission);
        }
    }
}
