using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.RBAC
{
    public class Subject
    {
        public Subject() 
        {
            this.Roles = new List<Role>();
            this.Permissions = new List<Permission>();
        }
        public Subject(int id, string name):this()
        {
            this.SubjectId = id;
            this.Name = name;
        }

        public int SubjectId { get; private set; }
        public string Name { get; private set; }

        public List<Role> Roles { get; private set; }
        public List<Permission> Permissions { get; private set; }

        public Subject Add(Role role)
        {
            this.Roles.Add(role);
            return this;
        }
        
        public Subject Add(IEnumerable<Role> roles)
        {
            this.Roles.AddRange(roles);
            return this;
        }

        public Subject Add(Permission permission)
        {
            this.Permissions.Add(permission);
            return this;
        }

        public Subject Add(IEnumerable<Permission> permissions)
        {
            this.Permissions.AddRange(permissions);
            return this;
        }

        public ISet<int> GetPermissions()
        {
            ISet<int> permissions = new HashSet<int>();
            foreach (Role role in this.Roles)
            {
                foreach (Permission permission in role.Permissions)
                {
                    permissions.Add(permission.PermissionId);
                }
            }
            return permissions;
        }

        public byte[] GetPermissionsBytes()
        {
            int max = this.Roles.Max(n => n.Permissions.Max(p => p.PermissionId))+1;
            BitArray permissions = new BitArray(max);
            foreach(Role role in this.Roles)
            {
                foreach(Permission permission in role.Permissions)
                {
                    permissions[permission.PermissionId] = true;
                }
            }
            return permissions.ToByte();
        }

        public bool HavePermission(int permission)
        {
            if (Permissions.Any(n => n.PermissionId == permission))
            {
                return true;
            }

            foreach (Role role in this.Roles)
            {
                if(role.Permissions.Any(n=>n.PermissionId == permission))
                {
                    return true;
                }
            }
            return false;
        }

        public string Debug(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}Subject Id: {1}, Name: {2}", new string(' ', indent), this.SubjectId, this.Name);
            foreach(var role in Roles)
            {
                sb.AppendFormat("\r\n{0}", role.Debug(indent + 1));
            }
            foreach (var permission in Permissions)
            {
                sb.AppendFormat("\r\n{0}", permission.Debug(indent + 1));
            }
            return sb.ToString();
        }
    }
}
