using CQRS.Demo.Contracts.Commands;
using CQRS.Demo.Model.Accounts;
using CQRS.Demo.Model.Investments;
using CQRS.Demo.Model.Projects;
using Grit.CQRS;
using Grit.CQRS.Exceptions;
using Grit.Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CQRS.Demo.Applications
{
    public class InvestmentAndPaymentApplication
    {
        public InvestmentAndPaymentApplication(
            IAccountService accountService,
            IProjectService projectService,
            IInvestmentService investmentService)
        {
            _accountService = accountService;
            _projectService = projectService;
            _investmentService = investmentService;
        }

        private IAccountService _accountService;
        private IProjectService _projectService;
        private IInvestmentService _investmentService;

        public void CreateInvestment(CreateInvestmentCommand command)
        {
            var account = _accountService.Get(command.AccountId);
            if (account.Amount < command.Amount)
            {
                throw new BusinessException("用户账户余额不足。");
            }

            var project = _projectService.Get(command.ProjectId);
            if (project.Amount < command.Amount)
            {
                throw new BusinessException("项目可投资金额不足。");
            }

            using (TransactionScope scope = new TransactionScope(
                    TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead }))
            {
                ServiceLocator.CommandBus.Send(command);
                scope.Complete();
            }
        }

        public void CompleteInvestment(CompleteInvestmentCommand command)
        {
            var investment = _investmentService.Get(command.InvestmentId);
            if (investment == null)
            {
                throw new BusinessException("投资不存在。");
            }
            if(investment.Status != Contracts.Enum.InvestmentStatus.Initial)
            {
                throw new BusinessException("投资已经支付。");
            }
            using (UnitOfWork u = new UnitOfWork())
            {
                ServiceLocator.CommandBus.Send(command);
                u.Complete();
            }
        }

        public void CreateAccount(CreateAccountCommand command)
        {
            using (UnitOfWork u = new UnitOfWork())
            {
                ServiceLocator.CommandBus.Send(command);
                u.Complete();
            }
        }
    }
}

