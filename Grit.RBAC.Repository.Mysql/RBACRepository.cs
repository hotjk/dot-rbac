using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Grit.RBAC.Repository.MySql
{
    public class RBACRepository : BaseRepository, IRBACRepository
    {
        public IEnumerable<Permission> GetPermissions()
        {
            using (IDbConnection connection = OpenConnection())
            {
                return connection.Query<Permission>("SELECT PermissionId, Name FROM rbac_permission;");
            }
        }

        public IEnumerable<Role> GetRoles()
        {
            using (IDbConnection connection = OpenConnection())
            {
                return connection.Query<Role>("SELECT RoleId, Name FROM rbac_role;");
            }
        }

        public IEnumerable<Role> GetRolesWithPermission()
        {
            using (IDbConnection connection = OpenConnection())
            {
                var roles = connection.Query<Role>("SELECT RoleId, Name FROM rbac_role;");
                var permissions = connection.Query<RolePermission>(
                    "SELECT rp.RoleId, p.PermissionId, p.Name FROM rbac_permission p " +
                    "JOIN rbac_role_permission rp ON p.PermissionId = rp.PermissionId;");
                foreach(var role in roles)
                {
                    role.Add(permissions
                        .Where(n=>n.RoleId == role.RoleId)
                        .Select(n=>new Permission(n.PermissionId, n.Name)));
                }
                return roles;
            }
        }

        public IEnumerable<Subject> GetSubjects()
        {
            using (IDbConnection connection = OpenConnection())
            {
                return connection.Query<Subject>("SELECT SubjectId, Name FROM rbac_subject;");
            }
        }

        public Permission GetPermission(int id)
        {
            using (IDbConnection connection = OpenConnection())
            {
                return connection.Query<Permission>(
                    "SELECT PermissionId, Name FROM rbac_permission WHERE PermissionId=@PermissionId;",
                    new { PermissionId = id }).SingleOrDefault();
            }
        }

        public Role GetRole(int id)
        {
            using (IDbConnection connection = OpenConnection())
            {
                Role role = connection.Query<Role>("SELECT RoleId, Name FROM rbac_role WHERE RoleId = @RoleId;", 
                    new { RoleId = id }).SingleOrDefault();
                var permissions = connection.Query<Permission>(
                    "SELECT p.PermissionId, p.Name FROM rbac_permission p " +
                    "JOIN rbac_role_permission rp ON p.PermissionId = rp.PermissionId WHERE rp.RoleId = @RoleId;",
                    new { RoleId = id });
                role.Add(permissions);
                return role;
            }
        }

        class RolePermission
        {
            public int PermissionId {get;set;}
            public string Name {get;set;}
            public int RoleId {get;set;}
        }
        public Subject GetSubject(int id)
        {
            using (IDbConnection connection = OpenConnection())
            {
                Subject subject = connection.Query<Subject>("SELECT SubjectId, Name FROM rbac_subject WHERE @SubjectId = SubjectId;", new { SubjectId = id }).SingleOrDefault();
                var roles = connection.Query<Role>(
                    "SELECT r.RoleId, r.Name FROM rbac_role r JOIN rbac_subject_role sr ON r.RoleId = sr.RoleId WHERE sr.SubjectId = @SubjectId;",
                    new { SubjectId = id });
                var maps = connection.Query<RolePermission>(
                    "SELECT p.PermissionId, p.Name, rp.RoleId FROM rbac_permission p " +
                    "JOIN rbac_role_permission rp ON p.PermissionId = rp.PermissionId " +
                    "JOIN rbac_subject_role sr ON rp.RoleId = sr.RoleId WHERE sr.SubjectId = @SubjectId;",
                    new { SubjectId = id });
                foreach(var role in roles)
                {
                    role.Add(maps.Where(n => n.RoleId == role.RoleId).Select(n=>new Permission(n.PermissionId, n.Name)));
                    subject.Add(role);
                }
                return subject;
            }
        }

        public bool SavePermission(Permission permission)
        {
            using (IDbConnection connection = OpenConnection())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    int n = connection.Execute("UPDATE rbac_permission SET Name = @Name WHERE PermissionId = @PermissionId;", permission);
                    if (n == 0)
                    {
                        n = connection.Execute("INSERT INTO rbac_permission (PermissionId, Name) VALUES (@PermissionId, @Name);", permission);
                    }
                    transaction.Commit();
                    return n == 1;
                }
            }
        }

        public bool SaveRole(Role role)
        {
            using (IDbConnection connection = OpenConnection())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    int n = connection.Execute("UPDATE rbac_role SET Name = @Name WHERE RoleId = @RoleId;", role);
                    if (n == 0)
                    {
                        n = connection.Execute("INSERT INTO rbac_role (RoleId, Name) VALUES (@RoleId, @Name);", role);
                    }
                    transaction.Commit();
                    return n == 1;
                }
            }
        }

        public bool SaveSubject(Subject subject)
        {
            using (IDbConnection connection = OpenConnection())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    int n = connection.Execute("UPDATE rbac_subject SET Name = @Name WHERE SubjectId = @SubjectId;", subject);
                    if (n == 0)
                    {
                        n = connection.Execute("INSERT INTO rbac_subject (SubjectId, Name) VALUES (@SubjectId, @Name);", subject);
                    }
                    transaction.Commit();
                    return n == 1;
                }
            }
        }

        public void SaveRolePermissions(Role role)
        {
            using (IDbConnection connection = OpenConnection())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    connection.Execute("DELETE FROM rbac_role_permission WHERE RoleId = @RoleId;", role);
                    connection.Execute("INSERT INTO rbac_role_permission (RoleId, PermissionId) VALUES (@RoleId, @PermissionId);",
                        role.Permissions.Select(n => new { RoleId = role.RoleId, PermissionId = n.PermissionId }));
                    transaction.Commit();
                }
            }
        }

        public void SaveRolePermissions(IEnumerable<Role> roles)
        {
            using (IDbConnection connection = OpenConnection())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    foreach (var role in roles)
                    {
                        connection.Execute("DELETE FROM rbac_role_permission WHERE RoleId = @RoleId;", role);
                        connection.Execute("INSERT INTO rbac_role_permission (RoleId, PermissionId) VALUES (@RoleId, @PermissionId);",
                            role.Permissions.Select(n => new { RoleId = role.RoleId, PermissionId = n.PermissionId }));
                    }
                    transaction.Commit();
                }
            }
        }

        public void SaveSubjectRoles(Subject subject)
        {
            using (IDbConnection connection = OpenConnection())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    connection.Execute("DELETE FROM rbac_subject_role WHERE SubjectId = @SubjectId;", subject);
                    connection.Execute("INSERT INTO rbac_subject_role (SubjectId, RoleId) VALUES (@SubjectId, @RoleId);",
                        subject.Roles.Select(n => new { SubjectId = subject.SubjectId, RoleId = n.RoleId }));
                    transaction.Commit();
                }
            }
        }
    }
}
