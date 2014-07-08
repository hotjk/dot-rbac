using CQRS.Demo.Applications;
using CQRS.Demo.Contracts.Calls;
using CQRS.Demo.Contracts.Commands;
using CQRS.Demo.Contracts.Events;
using CQRS.Demo.Model.Investments;
using CQRS.Demo.Web.Models;
using Grit.CQRS;
using Grit.CQRS.Calls;
using Grit.CQRS.Exceptions;
using Grit.Sequence;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CQRS.Demo.Web.Controllers
{
    public class InvestController : Controller
    {
        public InvestController(ISequenceService sequenceService, 
            InvestmentAndPaymentApplication application,
            IInvestmentService investmentService)
        {
            _application = application;
            _sequenceService = sequenceService;
            _investmentService = investmentService;
        }

        private ISequenceService _sequenceService;
        private InvestmentAndPaymentApplication _application;
        private IInvestmentService _investmentService;

        [HttpGet]
        public ActionResult Running(RunningViewModel vm)
        {
            return View(vm);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new InvestViewModel
            {
                ProjectId = 1,
                AccountId = 2,
                Amount = 100,
            });
        }

        [HttpPost]
        public ActionResult Create(InvestViewModel vm)
        {
            var call = new InvestmentCreateRequest
            {
                InvestmentId = _sequenceService.Next(SequenceID.CQRS_Investment, 1),
                AccountId = vm.AccountId,
                ProjectId = vm.ProjectId,
                Amount = vm.Amount
            };

            CallResponse response = ServiceLocator.CallBus.Send(call);
            TempData["CallResponse"] = response;
            return RedirectToAction("Index", new { id = call.InvestmentId });
        }

        public ActionResult Index(int id)
        {
            var investment = _investmentService.Get(id);
            return View(investment);
        }

        public ActionResult Pay(int id)
        {
            var call = new InvestmentPayRequest
            {
                InvestmentId = id
            };

            CallResponse response = ServiceLocator.CallBus.Send(call);
            TempData["CallResponse"] = response;
            return RedirectToAction("Index", new { id = call.InvestmentId });
        }
    }
}