using Grit.CQRS.Demo.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace Grit.CQRS.Demo.Repository.Write
{
    public class AccountWriteRepository : BaseRepository, IAccountWriteRepository
    {
        public bool Add(Account account)
        {
            using (IDbConnection connection = OpenConnection())
            {
                return 1 == connection.Execute("INSERT INTO cqrs_account (AccountId, Amount) VALUES (@AccountId, @Amount);",
                    account);
            }
        }

        public bool Update(Account account)
        {
            using (IDbConnection connection = OpenConnection())
            {
                return 1 == connection.Execute("UPDATE cqrs_account SET Amount = @Amount WHERE AccountId = @AccountId;",
                    account);
            }
        }
    }
}
