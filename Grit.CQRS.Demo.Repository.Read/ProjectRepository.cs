using Grit.CQRS.Demo.Model.Projects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Grit.CQRS.Demo.Repository.Read
{
    public class ProjectRepository : BaseRepository, IProjectRepository
    {
        public Project Get(int id)
        {
            using (IDbConnection connection = OpenConnection())
            {
                return connection.Query<Project>("SELECT ProjectId, Name, Amount FROM cqrs_project;",
                    new { id = id }).SingleOrDefault();
            }
        }
    }
}
