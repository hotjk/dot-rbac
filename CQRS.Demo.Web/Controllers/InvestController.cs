using CQRS.Demo.Applications;
using CQRS.Demo.Contracts.Commands;
using Grit.CQRS.Exceptions;
using Grit.Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CQRS.Demo.Web.Controllers
{
    public class InvestController : Controller
    {
        public InvestController(InvestmentAndPaymentApplication application,
            ISequenceService sequenceService)
        {
            _application = application;
            _sequenceService = sequenceService;
        }

        private InvestmentAndPaymentApplication _application;
        private ISequenceService _sequenceService;

        public ActionResult Create()
        {
            int accountId = 2;
            int projectId = 1;
            decimal amount = 100.00m;

            var command = new CreateInvestment
            {
                InvestmentId = _sequenceService.Next(SequenceID.CQRS_Investment, 1),
                AccountId = accountId,
                ProjectId = projectId,
                Amount = amount
            };
            try
            {
                _application.CreateInvestment(command);
            }
            catch(BusinessException ex)
            {
                return Content(ex.Message);
            }

            return RedirectToAction("Completed", new { id = command.InvestmentId });
        }

        public string Completed(int id)
        {
            var command = new CompleteInvestment
            {
                InvestmentId = id
            };
            try
            {
                _application.CompleteInvestment(command);
            }
            catch (BusinessException ex)
            {
                return ex.Message;
            }

            return "Bingo";
        }
    }
}