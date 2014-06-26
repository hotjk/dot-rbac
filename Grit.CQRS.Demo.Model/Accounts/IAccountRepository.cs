using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.Accounts
{
    public interface IAccountRepository
    {
        Account Get(int id);
    }
}
