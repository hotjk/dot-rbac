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
                return connection.Query<Permission>(
@"SELECT PermissionId, Name FROM rbac_permission;");
            }
        }

        public IEnumerable<Role> GetRoles(bool withPermission)
        {
            using (IDbConnection connection = OpenConnection())
            {
                var roles = connection.Query<Role>(
@"SELECT RoleId, Name FROM rbac_role;");
                if (withPermission) 
                {
                    var rp = connection.Query<PermissionAndId>(
@"SELECT p.PermissionId, p.Name, rp.RoleId AS Id
FROM rbac_permission p 
JOIN rbac_role_permission rp ON p.PermissionId = rp.PermissionId;");
                    foreach (var role in roles)
                    {
                        role.Add(rp
                            .Where(n => n.Id == role.RoleId)
                            .Select(n => new Permission(n.PermissionId, n.Name)));
                    }
                }
                return roles;
            }
        }

        public IEnumerable<Subject> GetSubjects()
        {
            using (IDbConnection connection = OpenConnection())
            {
                return connection.Query<Subject>(
@"SELECT SubjectId, Name FROM rbac_subject;");
            }
        }

        public Permission GetPermission(int id)
        {
            using (IDbConnection connection = OpenConnection())
            {
                return connection.Query<Permission>(
@"SELECT PermissionId, Name FROM rbac_permission WHERE PermissionId=@PermissionId;",
                    new { PermissionId = id }).SingleOrDefault();
            }
        }

        public Role GetRole(int id, bool withPermission)
        {
            using (IDbConnection connection = OpenConnection())
            {
                Role role = connection.Query<Role>(
@"SELECT RoleId, Name FROM rbac_role WHERE RoleId = @RoleId;", 
                    new { RoleId = id }).SingleOrDefault();
                if (withPermission)
                {
                    var permissions = connection.Query<Permission>(
    @"SELECT p.PermissionId, p.Name 
FROM rbac_permission p 
JOIN rbac_role_permission rp ON p.PermissionId = rp.PermissionId 
WHERE rp.RoleId = @RoleId;",
                        new { RoleId = id });
                    role.Add(permissions);
                }
                return role;
            }
        }

        class PermissionAndId
        {
            public int PermissionId {get;set;}
            public string Name {get;set;}
            public int Id {get;set;}
        }

        public Subject GetSubject(int id, bool withRole, bool withRolePermission, bool withPermission)
        {
            if (withRole == false && withRolePermission == true)
            {
                withRole = true;
            }

            using (IDbConnection connection = OpenConnection())
            {
                Subject subject = connection.Query<Subject>(
@"SELECT SubjectId, Name 
FROM rbac_subject 
WHERE @SubjectId = SubjectId;", new { SubjectId = id }).SingleOrDefault();
                if (withRole)
                {
                    var roles = connection.Query<Role>(
    @"SELECT r.RoleId, r.Name 
FROM rbac_role r 
JOIN rbac_subject_role sr ON r.RoleId = sr.RoleId 
WHERE sr.SubjectId = @SubjectId;",
                        new { SubjectId = id });
                    subject.Add(roles);

                    if (withRolePermission)
                    {
                        var maps = connection.Query<PermissionAndId>(
        @"SELECT p.PermissionId, p.Name, rp.RoleId AS Id
FROM rbac_permission p 
JOIN rbac_role_permission rp ON p.PermissionId = rp.PermissionId 
JOIN rbac_subject_role sr ON rp.RoleId = sr.RoleId 
WHERE sr.SubjectId = @SubjectId;", new { SubjectId = id });
                        foreach (var role in roles)
                        {
                            role.Add(maps.Where(n => n.Id == role.RoleId).Select(n => new Permission(n.PermissionId, n.Name)));
                        }
                    }
                }
                if (withPermission)
                {
                    var permissions = connection.Query<Permission>(
    @"SELECT p.PermissionId, p.Name
FROM rbac_permission p 
JOIN rbac_subject_permission rp ON p.PermissionId = sp.PermissionId 
WHERE sp.SubjectId = @SubjectId;", new { SubjectId = id });
                    subject.Permissions.AddRange(permissions);
                }
                return subject;
            }
        }
    }
}
