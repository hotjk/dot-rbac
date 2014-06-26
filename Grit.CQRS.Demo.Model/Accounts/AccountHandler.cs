using Grit.CQRS.Demo.Contracts.Events;
using Grit.CQRS.Demo.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.Accounts
{
    public class AccountHandler : 
        IEventHandler<InvestmentCreatedEvent>,
        ICommandHandler<InitAccountCommand>
    {
        private IAccountWriteRepository _repository;
        public AccountHandler(IAccountWriteRepository repository)
        {
            _repository = repository;
        }
        public void Handle(InvestmentCreatedEvent @event)
        {
            _repository.DecreaseAmount(@event.AccountId, @event.Amount);
        }

        public void Execute(InitAccountCommand command)
        {
            _repository.Init(new Account { AccountId = command.AccountId, Amount = 0.0m });
        }
    }
}
