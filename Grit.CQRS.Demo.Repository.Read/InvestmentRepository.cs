using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Grit.CQRS.Demo.Model.Investments;

namespace Grit.CQRS.Demo.Repository.Read
{
    public class InvestmentRepository : BaseRepository, IInvestmentRepository
    {
        public Investment Get(int id)
        {
            using (IDbConnection connection = OpenConnection())
            {
                return connection.Query<Investment>("SELECT InvestmentId, ProjectId, AccountId, Amount FROM cqrs_investment;",
                    new { id = id }).SingleOrDefault();
            }
        }
    }
}
