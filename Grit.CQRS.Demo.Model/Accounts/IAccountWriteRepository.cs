using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.Accounts
{
    public interface IAccountWriteRepository
    {
        bool Add(Account account);
        bool Update(Account account);
    }
}
