using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.Commands
{
    public class CreateItemCommand : Command
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public CreateItemCommand(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
