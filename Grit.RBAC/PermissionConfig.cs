using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.RBAC
{
    public enum PermissionID
    {
        Reserved = 0,
        Test1 = 1,
        Test100 = 100,
    }

    public static class PermissionConfig
    {
        public static readonly int MIN = 0;
        public static readonly int MAX;
        public static readonly int COUNT;
        private static readonly ICollection<Permission> permissions;

        static PermissionConfig()
        {
            var values = Enum.GetValues(typeof(PermissionID));
            MAX = (int)values.Cast<PermissionID>().Max();
            COUNT = values.Length;

            permissions = new List<Permission>()
            {
                new Permission((int)PermissionID.Reserved, "系统保留"),
                new Permission((int)PermissionID.Test1, "测试权限1"),
                new Permission((int)PermissionID.Test100, "测试权限100")
            };

            if (COUNT != permissions.Count)
            {
                throw new ApplicationException("权限列表初始化失败");
            }
        }
    }
}
