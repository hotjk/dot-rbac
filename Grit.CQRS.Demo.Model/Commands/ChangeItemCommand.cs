using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.Commands
{
    public class ChangeItemCommand : Command
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public ChangeItemCommand(string title, string description)
        {
            Description = description;
            Title = title;
        }
    }
}
