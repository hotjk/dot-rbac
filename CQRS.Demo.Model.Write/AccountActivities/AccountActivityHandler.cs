using CQRS.Demo.Contracts.Commands;
using CQRS.Demo.Model.AccountActivities;
using Grit.CQRS;
using Grit.CQRS.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Demo.Model.Write.AccountActivities
{
    public class AccountActivityHandler : ICommandHandler<CreateAccountActivityCommand>
    {
        static AccountActivityHandler()
        {
            AutoMapper.Mapper.CreateMap<CreateAccountActivityCommand, AccountActivity>();
        }
        public AccountActivityHandler(IAccountActivityWriteRepository repository)
        {
            _repository = repository;
        }
        private IAccountActivityWriteRepository _repository;
        public void Execute(CreateAccountActivityCommand command)
        {
            if(command.FromAccountId == null && command.ToAccountId == null)
            {
                throw new BusinessException("账户交易双方不能同时为空。");
            }
            _repository.Save(AutoMapper.Mapper.Map<AccountActivity>(command));
        }
    }
}
