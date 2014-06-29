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
        ICommandHandler<DecreaseProjectAmountCommand>
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

        public void Execute(InitProjectCommand command)
        {
            _repository.Init(AutoMapper.Mapper.Map<Project>(command));
        }

        public void Execute(DecreaseProjectAmountCommand command)
        {
            if (!_repository.DecreaseAmount(command.ProjectId, command.Amount))
            {
                throw new BusinessException("项目可投资金额不足。");
            }
        }
    }
}

