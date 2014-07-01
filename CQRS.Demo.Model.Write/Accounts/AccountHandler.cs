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
        ICommandHandler<ChangeAccountAmountCommand>
    {
        static AccountHandler()
        {
            AutoMapper.Mapper.CreateMap<ChangeAccountAmountCommand, AccountAmountChanged>();
        }
        private IAccountWriteRepository _repository;
        public AccountHandler(IAccountWriteRepository repository)
        {
            _repository = repository;
        }
        public void Execute(ChangeAccountAmountCommand command)
        {
            if (!_repository.ChangeAmount(command.AccountId, command.Change))
            {
                throw new BusinessException("账户余额不足。");
            }
            ServiceLocator.EventBus.Publish(AutoMapper.Mapper.Map<AccountAmountChanged>(command));
        }
    }
}
