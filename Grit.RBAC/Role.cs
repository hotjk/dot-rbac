using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.RBAC
{
    public class Role
    {
        public Role() 
        {
            this.Permissions = new List<Permission>();
        }
        public Role(int id, string name):this()
        {
            this.RoleId = id;
            this.Name = name;
        }

        public int RoleId { get; private set; }
        public string Name { get; private set; }
        public ICollection<Permission> Permissions { get; private set; }

        public Role Add(Permission permission)
        {
            this.Permissions.Add(permission);
            return this;
        }

        public Role Add(IEnumerable<Permission> permissions)
        {
            foreach(var permission in permissions)
            {
                this.Add(permission);
            }
            return this;
        }

        public bool HavePermission(int permission)
        {
            if (Permissions.Any(n => n.PermissionId == permission))
            {
                return true;
            }
            return false;
        }

        public string Debug(int indent=0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}Role Id: {1}, Name: {2}", new string(' ', indent), this.RoleId, this.Name);
            foreach (var permission in Permissions)
            {
                sb.AppendFormat("\r\n{0}", permission.Debug(indent+1));
            }
            return sb.ToString();
        }
    }
}
