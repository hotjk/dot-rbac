using Grit.CQRS.Demo.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Grit.CQRS.Demo.Repository.Read
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public Account Get(int id)
        {
            using (IDbConnection connection = OpenConnection())
            {
                return connection.Query<Account>("SELECT AccountId, Amount FROM cqrs_account;",
                    new { id = id }).SingleOrDefault();
            }
        }
    }
}
