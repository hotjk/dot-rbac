using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Demo.Model.Accounts
{
    public interface IAccountWriteRepository
    {
        bool Init(Account account);
        bool IncreaseAmount(int accountId, decimal Amount);
        bool DecreaseAmount(int accountId, decimal Amount);
    }
}
