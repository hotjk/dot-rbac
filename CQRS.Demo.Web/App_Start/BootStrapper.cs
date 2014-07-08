using CQRS.Demo.Model;
using CQRS.Demo.Model.Accounts;
using CQRS.Demo.Model.Investments;
using CQRS.Demo.Model.Projects;
using CQRS.Demo.Model.Write.AccountActivities;
using CQRS.Demo.Model.Write.Messages;
using CQRS.Demo.Repositories;
using CQRS.Demo.Repositories.Write;
using Grit.CQRS;
using Grit.Sequence;
using Grit.Sequence.Repository.MySql;
using Ninject;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Demo.Web
{
    public static class BootStrapper
    {
        public static IKernel Kernel { get; private set; }
        private static IConnection connection;
        private static IModel channel;

        public static void BootStrap()
        {
            AddIocBindings();
            InitMessageQueue();
            InitHandlerFactory();
            InitServiceLocator();
        }

        public static void Dispose()
        {
            if(channel != null)
            {
                channel.Dispose();
            }
            if(connection != null)
            {
                connection.Dispose();
            }
            if (Kernel != null)
            {
                Kernel.Dispose();
            }
        }

        private static void AddIocBindings()
        {
            Kernel = new StandardKernel();

            Kernel.Bind<ISequenceRepository>().To<SequenceRepository>().InSingletonScope();
            Kernel.Bind<ISequenceService>().To<SequenceService>().InSingletonScope();

            Kernel.Bind<IInvestmentRepository>().To<InvestmentRepository>().InSingletonScope();
            Kernel.Bind<IInvestmentWriteRepository>().To<InvestmentWriteRepository>().InSingletonScope();
            Kernel.Bind<IInvestmentService>().To<InvestmentService>().InSingletonScope();
            Kernel.Bind<IProjectRepository>().To<ProjectRepository>().InSingletonScope();
            Kernel.Bind<IProjectWriteRepository>().To<ProjectWriteRepository>().InSingletonScope();
            Kernel.Bind<IProjectService>().To<ProjectService>().InSingletonScope();
            Kernel.Bind<IAccountRepository>().To<AccountRepository>().InSingletonScope();
            Kernel.Bind<IAccountWriteRepository>().To<AccountWriteRepository>().InSingletonScope();
            Kernel.Bind<IAccountService>().To<AccountService>().InSingletonScope();
            Kernel.Bind<IMessageWriteRepository>().To<MessageWriteRepository>().InSingletonScope();
            Kernel.Bind<IAccountActivityWriteRepository>().To<AccountActivityWriteRepository>().InSingletonScope();
        }

        private static void InitMessageQueue()
        {
            //rabbitmqctl add_vhost event_bus_exchange
            //rabbitmqctl add_user event_user event_password
            //rabbitmqctl set_permissions -p event_bus_exchange event_user ".*" ".*" ".*"
            //rabbitmqctl set_permissions -p event_bus_exchange guest ".*" ".*" ".*"
            //rabbitmqctl list_queues -p event_bus_exchange
            //rabbitmq-plugins enable rabbitmq_management
            //http://localhost:15672
            ConnectionFactory factory = new ConnectionFactory { Uri = Grit.Configuration.RabbitMQ.CQRSDemo };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.ExchangeDeclare(Grit.Configuration.RabbitMQ.CQRSDemoEventBusExchange, ExchangeType.Topic, true);
            channel.QueueDeclare("account_event_queue", true, false, false, null);
            channel.QueueBind("account_event_queue", Grit.Configuration.RabbitMQ.CQRSDemoEventBusExchange, "account.*.*");

            channel.QueueDeclare(Grit.Configuration.RabbitMQ.CQRSDemoSagaQueue, true, false, false, null);
        }

        private static void InitHandlerFactory()
        {
            CommandHandlerFactory.Init(Kernel, new string[] { "CQRS.Demo.Contracts" },
                new string[] { "CQRS.Demo.Model.Write" });
            EventHandlerFactory.Init(Kernel, new string[] { "CQRS.Demo.Contracts" },
                new string[] { "CQRS.Demo.Model.Write", "CQRS.Demo.Applications" }, channel,
                Grit.Configuration.RabbitMQ.CQRSDemoEventBusExchange);
            ActionHandlerFactory.Init(Kernel, new string[] { "CQRS.Demo.Contracts" },
                new string[] { "CQRS.Demo.Applications" }, channel,
                Grit.Configuration.RabbitMQ.CQRSDemoSagaQueue);
        }

        private static void InitServiceLocator()
        {
            ServiceLocator.Init(Kernel);
        }
    }
}
