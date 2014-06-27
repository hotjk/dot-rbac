using CQRS.Demo.Model.Investments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;


namespace CQRS.Demo.Repositories.Write
{
    public class InvestmentWriteRepository : BaseRepository, IInvestmentWriteRepository
    {
        public bool Add(Investment investment)
        {
            using (IDbConnection connection = OpenConnection())
            {
                return 1 == connection.Execute("INSERT INTO cqrs_demo_investment (InvestmentId, ProjectId, AccountId, Amount) VALUES (@InvestmentId, @ProjectId, @AccountId, @Amount);",
                    investment);
            }
        }
    }
}
