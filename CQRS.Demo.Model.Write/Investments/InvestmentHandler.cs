using CQRS.Demo.Contracts.Events;
using CQRS.Demo.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grit.CQRS;
using CQRS.Demo.Model.Investments;

namespace CQRS.Demo.Model.Investments
{
    public class InvestmentHandler : 
        ICommandHandler<CreateInvestmentCommand>,
        ICommandHandler<CompleteInvestmentCommand>
    {
        static InvestmentHandler()
        {
            AutoMapper.Mapper.CreateMap<CreateInvestmentCommand, Investment>();
            AutoMapper.Mapper.CreateMap<CreateInvestmentCommand, InvestmentStatusCreated>();
            AutoMapper.Mapper.CreateMap<Investment, DecreaseAccountAmountCommand>();
            AutoMapper.Mapper.CreateMap<Investment, DecreaseProjectAmountCommand>();
            AutoMapper.Mapper.CreateMap<Investment, InvestmentStatusCompleted>();
        }

        private IInvestmentWriteRepository _repository;
        public InvestmentHandler(IInvestmentWriteRepository repository)
        {
            _repository = repository;
        }

        public void Execute(CreateInvestmentCommand command)
        {
            _repository.Add(AutoMapper.Mapper.Map<Investment>(command));

            ServiceLocator.EventBus.Publish(AutoMapper.Mapper.Map<InvestmentStatusCreated>(command));
        }

        public void Execute(CompleteInvestmentCommand command)
        {
            Investment investment = _repository.GetForUpdate(command.InvestmentId);
            _repository.Complete(command.InvestmentId);
            ServiceLocator.CommandBus.Send(AutoMapper.Mapper.Map<DecreaseAccountAmountCommand>(investment));
            ServiceLocator.CommandBus.Send(AutoMapper.Mapper.Map<DecreaseProjectAmountCommand>(investment));
            ServiceLocator.EventBus.Publish(AutoMapper.Mapper.Map<InvestmentStatusCompleted>(investment));
        }
    }
}
