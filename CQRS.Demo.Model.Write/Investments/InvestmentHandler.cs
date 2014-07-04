using CQRS.Demo.Contracts.Events;
using CQRS.Demo.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grit.CQRS;
using CQRS.Demo.Model.Investments;
using CQRS.Demo.Model.Projects;

namespace CQRS.Demo.Model.Investments
{
    public class InvestmentHandler :
        ICommandHandler<CreateInvestment>,
        ICommandHandler<CompleteInvestment>
    {
        static InvestmentHandler()
        {
            AutoMapper.Mapper.CreateMap<CreateInvestment, Investment>();
            AutoMapper.Mapper.CreateMap<CreateInvestment, InvestmentStatusCreated>();
            AutoMapper.Mapper.CreateMap<Investment, InvestmentStatusCompleted>();
        }

        private IInvestmentWriteRepository _repository;
        private IProjectService _projectService;

        public InvestmentHandler(IInvestmentWriteRepository repository,
            IProjectService projectService)
        {
            _repository = repository;
            _projectService = projectService;
        }

        public void Execute(CreateInvestment command)
        {
            _repository.Add(AutoMapper.Mapper.Map<Investment>(command));

            ServiceLocator.EventBus.Publish(AutoMapper.Mapper.Map<InvestmentStatusCreated>(command));
        }

        public void Execute(CompleteInvestment command)
        {
            Investment investment = _repository.GetForUpdate(command.InvestmentId);
            
            _repository.Complete(command.InvestmentId);

            ServiceLocator.EventBus.Publish(AutoMapper.Mapper.Map<InvestmentStatusCompleted>(investment));
        }
    }
}
