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
        ICommandHandler<CreateInvestmentCommand>,
        ICommandHandler<CompleteInvestmentCommand>
    {
        static InvestmentHandler()
        {
            AutoMapper.Mapper.CreateMap<CreateInvestmentCommand, Investment>();
            AutoMapper.Mapper.CreateMap<CreateInvestmentCommand, InvestmentStatusCreated>();
            AutoMapper.Mapper.CreateMap<Investment, ChangeAccountAmountCommand>();
            AutoMapper.Mapper.CreateMap<Investment, ChangeProjectAmountCommand>();
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

        public void Execute(CreateInvestmentCommand command)
        {
            _repository.Add(AutoMapper.Mapper.Map<Investment>(command));

            ServiceLocator.EventBus.Publish(AutoMapper.Mapper.Map<InvestmentStatusCreated>(command));
        }

        public void Execute(CompleteInvestmentCommand command)
        {
            Investment investment = _repository.GetForUpdate(command.InvestmentId);
            Project project = _projectService.Get(investment.ProjectId);
            _repository.Complete(command.InvestmentId);

            ServiceLocator.CommandBus
                .Send(new ChangeProjectAmountCommand
                {
                    ProjectId = project.ProjectId,
                    Change = 0 - investment.Amount
                })
                .Send(new ChangeAccountAmountCommand
                {
                    AccountId = investment.AccountId,
                    Change = 0 - investment.Amount
                })
                .Send(new ChangeAccountAmountCommand
                {
                    AccountId = project.BorrowerId,
                    Change = investment.Amount
                })
                .Send(new CreateAccountActivityCommand
                {
                    FromAccountId = investment.AccountId,
                    ToAccountId = project.BorrowerId,
                    Amount = investment.Amount
                });

            ServiceLocator.EventBus.Publish(AutoMapper.Mapper.Map<InvestmentStatusCompleted>(investment));
        }
    }
}
