using CQRS.Demo.Contracts;
using CQRS.Demo.Contracts.Events;
using Grit.CQRS;
using Grit.CQRS.Exceptions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Demo.Sagas
{
    class Program
    {
        static void Main(string[] args)
        {
            using (RedisClient redis = new RedisClient())
            {
                redis.Set<string>("hello", "world");
                var world = redis.Get<string>("hello");
                Console.WriteLine(world);
            }

            log4net.Config.XmlConfigurator.Configure();
            // Pike a dummy method to ensoure Command/Event assembly been loaded
            CQRS.Demo.Contracts.EnsoureAssemblyLoaded.Pike();
            CQRS.Demo.Applications.EnsoureAssemblyLoaded.Pike();
            BootStrapper.BootStrap();

            var factory = new ConnectionFactory() { Uri = Grit.Configuration.RabbitMQ.CQRSDemo };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume("saga_event_queue", false, consumer);

                    while (true)
                    {
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        var routingKey = ea.RoutingKey;
                        channel.BasicAck(ea.DeliveryTag, false);
                        Console.WriteLine("---- '{0}':'{1}'", routingKey, message);

                        Type type = ServiceLocator.EventBus.GetEventType(Grit.CQRS.Event.ToCamelString(routingKey));
                        dynamic @event = JsonConvert.DeserializeObject(message,type);

                        try
                        {
                            ServiceLocator.EventBus.DirectHandle(@event);
                        }
                        catch (BusinessException ex)
                        {
                            Console.WriteLine(ex.Message);
                            using (RedisClient redis = new RedisClient())
                            {
                                redis.Set<string>(@event.Id.ToString(), ex.Message, DateTime.Now.AddHours(1));
                            }
                        }
                    }
                }
            }
        }
    }
}
