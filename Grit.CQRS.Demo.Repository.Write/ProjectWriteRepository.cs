using Grit.CQRS.Demo.Model.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace Grit.CQRS.Demo.Repository.Write
{
    public class ProjectWriteRepository : BaseRepository, IProjectWriteRepository
    {
        public bool Init(Project project)
        {
            using (IDbConnection connection = OpenConnection())
            {
                return 1 == connection.Execute("INSERT INTO cqrs_project (ProjectId, Name, Amount) VALUES (@ProjectId, @Name, @Amount);", 
                    project);
            }
        }

        public bool DecreaseAmount(int projectId, decimal amount)
        {
            using (IDbConnection connection = OpenConnection())
            {
                return 1 == connection.Execute("UPDATE cqrs_project SET Amount = Amount + @Amount WHERE ProjectId = @ProjectId AND Amount + @Amount >= 0;",
                    new { ProjectId = projectId, Amount = 0 - amount });
            }
        }
    }
}
