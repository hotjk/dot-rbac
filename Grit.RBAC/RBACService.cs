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

        public IEnumerable<Role> GetRoles()
        {
            return RBACRepository.GetRoles();
        }

        public IEnumerable<Subject> GetSubjects()
        {
            return RBACRepository.GetSubjects();
        }

        public Permission GetPermission(int id)
        {
            return RBACRepository.GetPermission(id);
        }

        public Role GetRole(int id)
        {
            return RBACRepository.GetRole(id);
        }

        public Subject GetSubject(int id)
        {
            return RBACRepository.GetSubject(id);
        }

        public bool SavePermission(Permission permission)
        {
            return RBACRepository.SavePermission(permission);
        }

        public bool SaveRole(Role role)
        {
            return RBACRepository.SaveRole(role);
        }

        public bool SaveSubject(Subject subject)
        {
            return RBACRepository.SaveSubject(subject);
        }

        public void SaveRolePermissions(Role role)
        {
            RBACRepository.SaveRolePermissions(role);
        }

        public void SaveSubjectRoles(Subject subject)
        {
            RBACRepository.SaveSubjectRoles(subject);
        }
    }
}
