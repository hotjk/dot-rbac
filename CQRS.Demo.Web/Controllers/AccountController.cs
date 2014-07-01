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
    public class AccountController : Controller
    {
        public AccountController(InvestmentAndPaymentApplication application,
            ISequenceService sequenceService)
        {
            _application = application;
            _sequenceService = sequenceService;
        }

        private InvestmentAndPaymentApplication _application;
        private ISequenceService _sequenceService;

        public ActionResult Create()
        {
            var command = new CreateAccountCommand
            {
                AccountId = _sequenceService.Next(SequenceID.CQRS_Account, 1),
            };
            try
            {
                _application.CreateAccount(command);
            }
            catch (BusinessException ex)
            {
                return Content(ex.Message);
            }

            return Content("Bingo");
        }
    }
}