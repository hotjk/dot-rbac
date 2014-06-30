using CQRS.Demo.Contracts.Events;
using CQRS.Demo.Model.Messages;
using Grit.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Demo.Model.Write.Messages
{
    public class MessageHandler : 
        IEventHandler<InvestmentCreatedEvent>,
        IEventHandler<AccountAmountChangedEvent>
    {
        private IMessageWriteRepository _repository;
        public MessageHandler(IMessageWriteRepository repository)
        {
            _repository = repository;
        }

        public void Handle(InvestmentCreatedEvent handle)
        {
            _repository.Add(new Message
            {
                AccountId = handle.AccountId,
                Content = string.Format("投资成功，投资金额{0:n}元。", handle.Amount)
            });
        }

        public void Handle(AccountAmountChangedEvent handle)
        {
            _repository.Add(new Message
            {
                AccountId = handle.AccountId,
                Content = string.Format("账户变动，变动金额{0:n}元。", handle.Amount)
            });
        }
    }
}
