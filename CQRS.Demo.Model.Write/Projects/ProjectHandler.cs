using CQRS.Demo.Contracts.Events;
using CQRS.Demo.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grit.CQRS;
using Grit.CQRS.Exceptions;

namespace CQRS.Demo.Model.Projects
{
    public class ProjectHandler : 
        ICommandHandler<InitProjectCommand>,
        IEventHandler<InvestmentCompletedEvent>
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

        public void Handle(InvestmentCompletedEvent @event)
        {
            if(!_repository.DecreaseAmount(@event.AccountId, @event.Amount))
            {
                throw new BusinessException("项目可投资金额不足。");
            }
        }

        public void Execute(InitProjectCommand command)
        {
            _repository.Init(AutoMapper.Mapper.Map<Project>(command));
        }
    }
}

