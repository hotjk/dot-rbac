using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.RBAC
{
    public class Permission
    {
        public Permission() { }
        public Permission(int id, string name)
        {
            this.PermissionId = id;
            this.Name = name;
        }
        public int PermissionId { get; private set; }
        public string Name { get; private set; }

        public string Debug(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}Permission Id: {1}, Name: {2}", new string(' ', indent), this.PermissionId, this.Name);
            return sb.ToString();
        }
    }
}
