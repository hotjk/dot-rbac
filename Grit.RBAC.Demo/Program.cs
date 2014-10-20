using Grit.RBAC.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Grit.RBAC;

namespace Grit.RBAC.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            BootStrapper.BootStrap(new StandardKernel());
            GetPermissionsTest();
            GetRolesTest();
            GetSubjectsTest();
            
            GetPermissionTest();
            GetRoleTest();
            GetSubjectTest();

            SaveRolePermissionsTest();
            SaveRolePermissionsTest();

            SaveSubjectRolesTest();
            SaveSubjectRolesTest();
        }

        private static void GetPermissionsTest()
        {
            Console.WriteLine("\r\n" + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            IRBACService service = BootStrapper.Kernel.Get<IRBACService>();
            var permissions = service.GetPermissions();
            foreach (var item in permissions)
            {
                Console.WriteLine(item.Debug());
            }
        }

        private static void GetRolesTest()
        {
            Console.WriteLine("\r\n" + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            IRBACService service = BootStrapper.Kernel.Get<IRBACService>();
            var roles = service.GetRoles();
            foreach (var item in roles)
            {
                Console.WriteLine(item.Debug());
            }
        }

        private static void GetSubjectsTest()
        {
            Console.WriteLine("\r\n" + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            IRBACService service = BootStrapper.Kernel.Get<IRBACService>();
            var subjects = service.GetSubjects();
            foreach (var item in subjects)
            {
                Console.WriteLine(item.Debug());
            }
        }

        private static void GetPermissionTest()
        {
            Console.WriteLine("\r\n" + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            IRBACService service = BootStrapper.Kernel.Get<IRBACService>();
            var item = service.GetPermission(1);
            Console.WriteLine(item.Debug());
        }

        private static void GetRoleTest()
        {
            Console.WriteLine("\r\n" + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            IRBACService service = BootStrapper.Kernel.Get<IRBACService>();
            var item = service.GetRole(1);
            Console.WriteLine(item.Debug());
        }

        private static void GetSubjectTest()
        {
            Console.WriteLine("\r\n" + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            IRBACService service = BootStrapper.Kernel.Get<IRBACService>();
            var item = service.GetSubject(3);
            Console.WriteLine(item.Debug());
            Console.WriteLine(item.GetPermissions().Print());
        }

        private static void SaveRolePermissionsTest()
        {
            Console.WriteLine("\r\n" + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            IRBACService service = BootStrapper.Kernel.Get<IRBACService>();
            var role = service.GetRole(1);
            var permissions = service.GetPermissions();
            var p = role.Permissions.FirstOrDefault(n=>n.PermissionId == 10);
            if(p != null)
            {
                role.Permissions.Remove(p);
            }
            else
            {
                role.Permissions.Add(permissions.FirstOrDefault(n => n.PermissionId == 10));
            }
            service.SaveRolePermissions(role);
            role = service.GetRole(1);
            Console.WriteLine(role.Debug());
        }

        private static void SaveSubjectRolesTest()
        {
            Console.WriteLine("\r\n" + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            IRBACService service = BootStrapper.Kernel.Get<IRBACService>();
            var subject = service.GetSubject(2);
            var roles = service.GetRoles();
            var p = subject.Roles.FirstOrDefault(n => n.RoleId == 1);
            if (p != null)
            {
                subject.Roles.Remove(p);
            }
            else
            {
                subject.Roles.Add(roles.FirstOrDefault(n => n.RoleId == 1));
            }
            service.SaveSubjectRoles(subject);
            subject = service.GetSubject(1);
            Console.WriteLine(subject.Debug());
        }
    }
}
