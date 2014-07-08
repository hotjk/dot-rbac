using Grit.CQRS.Calls;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Framing.v0_9_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grit.CQRS
{
    public class CallBus : ICallBus
    {
        private ICallHandlerFactory _callHandlerFactory;
        private string _replyQueueName = null;
        private QueueingBasicConsumer _consumer = null;

        public CallBus(ICallHandlerFactory CallHandlerFactory)
        {
            _callHandlerFactory = CallHandlerFactory;
        }

        public void Invoke<T>(T call) where T : Call
        {
            var handler = _callHandlerFactory.GetHandler<T>();
            if (handler != null)
            {
                handler.Invoke(call);
            }
        }

        public Type GetType(string name)
        {
            return _callHandlerFactory.GetType(name);
        }

        public string GetQueue()
        {
            return _callHandlerFactory.GetQueue();
        }

        private void DelcareReplyQueue()
        {
            if(_replyQueueName == null)
            {
                var channel = _callHandlerFactory.GetChannel();
                string name = channel.QueueDeclare();
                _consumer = new QueueingBasicConsumer(channel);
                channel.BasicConsume(name, true, _consumer);
                _replyQueueName = name;
            }
            else
            {
                // todo: empty queue
            }
        }

        public CallResponse Send<T>(T call) where T : Call
        {
            string json = JsonConvert.SerializeObject(call);
            log4net.LogManager.GetLogger("call.logger").Info(
                string.Format("{0}{1}{2}",
                call, Environment.NewLine,
                json));
            
            var channel = _callHandlerFactory.GetChannel();

            DelcareReplyQueue();

            var props = channel.CreateBasicProperties();
            props.ReplyTo = _replyQueueName;
            props.CorrelationId = call.Id.ToString();
            props.DeliveryMode = 2;
            props.Type = call.GetType().Name;
           
            channel.BasicPublish(string.Empty,
                _callHandlerFactory.GetQueue(),
                props,
                Encoding.UTF8.GetBytes(json));

            BasicDeliverEventArgs result;
            if (_consumer.Queue.Dequeue(10000, out result))
            {
                return JsonConvert.DeserializeObject<CallResponse>(Encoding.UTF8.GetString(result.Body));
            }
            throw new ApplicationException();
        }
    }
}
