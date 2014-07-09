using CQRS.Demo.Contracts;
using CQRS.Demo.Contracts.Events;
using Grit.CQRS;
using Grit.CQRS.Actions;
using Grit.CQRS.Exceptions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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
                    channel.BasicConsume(ServiceLocator.ActionBus.GetQueue(), false, consumer);

                    while (true)
                    {
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        var routingKey = ea.RoutingKey;
                        var props = ea.BasicProperties;
                        var replyProps = channel.CreateBasicProperties();
                        replyProps.CorrelationId = props.CorrelationId;
                        Type type = ServiceLocator.ActionBus.GetType(props.Type);
                        dynamic action = JsonConvert.DeserializeObject(message, type);
                        ActionResponse response = new ActionResponse { Result = ActionResponse.ActionResponseResult.OK };
                        //Console.WriteLine("{0} {1}", routingKey, message);

                        try
                        {
                            ServiceLocator.ActionBus.Invoke(action);
                        }
                        catch (BusinessException ex)
                        {
                            response.Result = ActionResponse.ActionResponseResult.NG;
                            response.Message = ex.Message;
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            log4net.LogManager.GetLogger("exception.logger").Error(ex);
                        }
                        finally
                        {
                            var responseBytes =
                            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
                            channel.BasicPublish("", props.ReplyTo, replyProps, responseBytes);
                            channel.BasicAck(ea.DeliveryTag, false);
                        }
                    }
                }
            }
        }
    }
}
