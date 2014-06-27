using CQRS.Demo.Contracts.Events;
using CQRS.Demo.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grit.CQRS;

namespace CQRS.Demo.Model.Projects.Handlers
{
    public class ProjectHandler : 
        ICommandHandler<InitProjectCommand>,
        IEventHandler<InvestmentCreatedEvent>
    {
        static ProjectHandler()
        {
            AutoMapper.Mapper.CreateMap<InitProjectCommand, Project>();
        }

        private IProjectWriteRepository _repository;
        public ProjectHandler(IProjectWriteRepository repository)
        {
            _repository = repository;
        }

        public void Handle(InvestmentCreatedEvent @event)
        {
            _repository.DecreaseAmount(@event.AccountId, @event.Amount);
        }

        public void Execute(InitProjectCommand command)
        {
            _repository.Init(AutoMapper.Mapper.Map<Project>(command));
        }
    }
}

