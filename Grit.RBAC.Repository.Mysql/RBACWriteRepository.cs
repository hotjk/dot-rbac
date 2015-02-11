using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Grit.RBAC.Repository.MySql
{
    public class RBACWriteRepository : BaseRepository, IRBACWriteRepository
    {
        public bool SavePermission(Permission permission)
        {
            using (IDbConnection connection = OpenConnection())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    int n = connection.Execute(
@"UPDATE rbac_permission SET Name = @Name 
WHERE PermissionId = @PermissionId;", permission);
                    if (n == 0)
                    {
                        n = connection.Execute(
@"INSERT INTO rbac_permission (PermissionId, Name) 
VALUES (@PermissionId, @Name);", permission);
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
                    int n = connection.Execute(
@"UPDATE rbac_role SET Name = @Name 
WHERE RoleId = @RoleId;", role);
                    if (n == 0)
                    {
                        n = connection.Execute(
@"INSERT INTO rbac_role (RoleId, Name) 
VALUES (@RoleId, @Name);", role);
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
                    int n = connection.Execute(
@"UPDATE rbac_subject SET Name = @Name 
WHERE SubjectId = @SubjectId;", subject);
                    if (n == 0)
                    {
                        n = connection.Execute(
@"INSERT INTO rbac_subject (SubjectId, Name) 
VALUES (@SubjectId, @Name);", subject);
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
                    connection.Execute(
@"DELETE FROM rbac_role_permission 
WHERE RoleId = @RoleId;", role);
                    connection.Execute(
@"INSERT INTO rbac_role_permission (RoleId, PermissionId) 
VALUES (@RoleId, @PermissionId);",
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
                        connection.Execute(
@"DELETE FROM rbac_role_permission 
WHERE RoleId = @RoleId;", role);
                        connection.Execute(
@"INSERT INTO rbac_role_permission (RoleId, PermissionId) 
VALUES (@RoleId, @PermissionId);",
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
                    connection.Execute(
@"DELETE FROM rbac_subject_role 
WHERE SubjectId = @SubjectId;", subject);
                    connection.Execute(
@"INSERT INTO rbac_subject_role (SubjectId, RoleId) 
VALUES (@SubjectId, @RoleId);",
                        subject.Roles.Select(n => new { SubjectId = subject.SubjectId, RoleId = n.RoleId }));
                    transaction.Commit();
                }
            }
        }

        public void SaveSubjectPermissions(Subject subject)
        {
            using (IDbConnection connection = OpenConnection())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    connection.Execute(
@"DELETE FROM rbac_subject_permission
WHERE SubjectId = @SubjectId;", subject);
                    connection.Execute(
@"INSERT INTO rbac_subject_permission (SubjectId, PermissionId)
VALUES (@SubjectId, @PermissionId;", subject.Permissions);
                    transaction.Commit();
                }
            }
        }
    }
}
