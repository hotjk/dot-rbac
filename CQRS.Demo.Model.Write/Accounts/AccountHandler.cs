using CQRS.Demo.Contracts.Events;
using CQRS.Demo.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grit.CQRS;
using Grit.CQRS.Exceptions;

namespace CQRS.Demo.Model.Accounts
{
    public class AccountHandler :
        ICommandHandler<DecreaseAccountAmountCommand>
    {
        static AccountHandler()
        {
            AutoMapper.Mapper.CreateMap<DecreaseAccountAmountCommand, AccountAmountChangedEvent>();
        }
        private IAccountWriteRepository _repository;
        public AccountHandler(IAccountWriteRepository repository)
        {
            _repository = repository;
        }
        public void Execute(DecreaseAccountAmountCommand command)
        {
            if (!_repository.DecreaseAmount(command.AccountId, command.Amount))
            {
                throw new BusinessException("账户余额不足。");
            }
            ServiceLocator.EventBus.Publish(AutoMapper.Mapper.Map<AccountAmountChangedEvent>(command));
        }
    }
}
