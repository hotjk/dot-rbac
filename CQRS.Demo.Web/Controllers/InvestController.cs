using CQRS.Demo.Applications;
using CQRS.Demo.Contracts.Commands;
using CQRS.Demo.Contracts.Events;
using CQRS.Demo.Model.Investments;
using CQRS.Demo.Web.Models;
using Grit.CQRS;
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

        //[HttpPost]
        //public ActionResult Create(InvestViewModel vm)
        //{
        //    var @event = new InvestmentRequestCreated
        //    {
        //        InvestmentId = _sequenceService.Next(SequenceID.CQRS_Investment, 1),
        //        AccountId = vm.AccountId,
        //        ProjectId = vm.ProjectId,
        //        Amount = vm.Amount
        //    };

        //    ServiceLocator.EventBus.Flush(@event);

        //    return RedirectToAction("Running", new RunningViewModel
        //    {
        //         C = "invest",
        //         A = "index",
        //         Id = @event.InvestmentId,
        //         E = @event.Id
        //    });
        //}

        [HttpPost]
        public async Task<ActionResult> Create(InvestViewModel vm)
        {
            var @event = new InvestmentRequestCreated
            {
                InvestmentId = _sequenceService.Next(SequenceID.CQRS_Investment, 1),
                AccountId = vm.AccountId,
                ProjectId = vm.ProjectId,
                Amount = vm.Amount
            };

            ServiceLocator.EventBus.Flush(@event);

            await Task.Delay(1000);

            return RedirectToAction("Index", new 
            {
                id = @event.InvestmentId,
                e = @event.Id
            });
        }

        public ActionResult Index(int id, string e)
        {
            var investment = _investmentService.Get(id);
            if(investment == null)
            {
                using (RedisClient redis = new RedisClient())
                {
                    var message = redis.Get<string>(e);
                    return Content(message);
                }
            }
            return View(investment);
        }

        public ActionResult Pay(int id)
        {
            var @event = new InvestmentOrderPaied
            {
                InvestmentId = id
            };

            ServiceLocator.EventBus.Flush(@event);

            return RedirectToAction("Running", new RunningViewModel
            {
                C = "invest",
                A = "index",
                Id = @event.InvestmentId,
                E = @event.Id,
            });
        }

        public ActionResult Paying(int id, string e)
        {
            ViewBag.Id = id;
            var investment = _investmentService.Get(id);
            if (investment == null || investment.Status != Contracts.Enum.InvestmentStatus.Paied)
            {
                using (RedisClient redis = new RedisClient())
                {
                    var message = redis.Get<string>(e);
                    return Content(message);
                }
            }
            return View(investment);
        }
    }
}