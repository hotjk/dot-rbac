using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.Services
{
    public interface IItemService
    {
        Item GetItem(int id);
    }
}
