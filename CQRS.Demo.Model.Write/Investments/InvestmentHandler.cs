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
            AutoMapper.Mapper.CreateMap<Investment, ChangeAccountAmount>();
            AutoMapper.Mapper.CreateMap<Investment, ChangeProjectAmount>();
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
            Project project = _projectService.Get(investment.ProjectId);
            _repository.Complete(command.InvestmentId);

            ServiceLocator.CommandBus
                .Send(new ChangeProjectAmount
                {
                    ProjectId = project.ProjectId,
                    Change = 0 - investment.Amount
                })
                .Send(new ChangeAccountAmount
                {
                    AccountId = investment.AccountId,
                    Change = 0 - investment.Amount
                })
                .Send(new ChangeAccountAmount
                {
                    AccountId = project.BorrowerId,
                    Change = investment.Amount
                })
                .Send(new CreateAccountActivity
                {
                    FromAccountId = investment.AccountId,
                    ToAccountId = project.BorrowerId,
                    Amount = investment.Amount
                });

            ServiceLocator.EventBus.Publish(AutoMapper.Mapper.Map<InvestmentStatusCompleted>(investment));
        }
    }
}
