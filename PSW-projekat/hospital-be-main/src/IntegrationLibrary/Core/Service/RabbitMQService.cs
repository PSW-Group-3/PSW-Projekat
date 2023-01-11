using IntegrationLibrary.Core.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Core.Service
{

    public class RabbitMQService : IRabbitMQService
    {
        public RabbitMQService() {}
        public void Send()
        {
            News n1 = new News("Title 1", "Text1", DateTime.Now, 2,"");
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("News",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var message = new { Name = "Producer", Message = n1.ToString() };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish("", "News", null, body);
        }
        public void SendScheduledOrder(ScheduledOrder scheduledOrder)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("scheduledOrdersQueue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            //var message = scheduledOrder;
            //String message = $"day:{scheduledOrder.DayOfMonth};aplus:{scheduledOrder.APlus};bplus:{scheduledOrder.BPlus};bankEmail:{scheduledOrder.BankEmail};hospitalEmail:{scheduledOrder.HospitalEmail};api:{scheduledOrder.BankApiKey}";
            var message = JsonConvert.SerializeObject(scheduledOrder);
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish("", "scheduledOrdersQueue", null, body);
        }

        public List<News> Recive(List<BloodBank> bloodBanks)
        {
            List<News> newses = new List<News>();
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("newsQueue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
                dynamic stuff = JsonConvert.DeserializeObject(message);
                try
                {
                    News n = new News();
                    n.Title = stuff.title;
                    n.Text = stuff.text;
                    n.Status = NewsStatus.PENDING;
                    if (checkBloodBankExists((string)stuff.bloodBank.email, bloodBanks) &&
                    checkBloodBankApiKey((string)stuff.bloodBank.email, (string)stuff.bloodBank.apikey, bloodBanks))
                    {
                        newses.Add(n);
                    }
                } catch
                {
                    throw;
                }
            };
            channel.BasicConsume("newsQueue", true, consumer);

            return newses;

        }

        public List<FilledOrder> ReciveSheduledOrders(List<BloodBank> bloodBanks)
        {
            List<FilledOrder> filledOrders = new List<FilledOrder>();
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("sentOrdersQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("message:");
                Console.WriteLine(message);
                dynamic stuff = JsonConvert.DeserializeObject(message);
                Console.WriteLine("stuff: ");
                Console.WriteLine(stuff);
                try
                {
                    FilledOrder filledOrder = new FilledOrder();
                    filledOrder.APlus = stuff.aplus;
                    filledOrder.BPlus = stuff.bplus;
                    filledOrder.ABPlus = stuff.abplus;
                    filledOrder.OPlus = stuff.oplus;

                    filledOrder.AMinus= stuff.aminus;
                    filledOrder.BMinus = stuff.bminus;
                    filledOrder.ABMinus = stuff.abminus;
                    filledOrder.OMinus = stuff.ominus;
                    filledOrder.BankEmail = stuff.bankEmail;
                    filledOrder.IsSent = stuff.sent;
                    if (checkBloodBankExists((string)stuff.bankEmail, bloodBanks) &&
                    checkBloodBankApiKey((string)stuff.bankEmail, (string)stuff.bankApi, bloodBanks))
                    {
                        filledOrders.Add(filledOrder);
                    }
                }
                catch
                {
                    throw;
                }
            };
            channel.BasicConsume("sentOrdersQueue", true, consumer);


            return filledOrders;

        }
        public bool checkBloodBankApiKey(string bankEmail, string bankApiKey, List<BloodBank> bloodBanks)
        {
            foreach (BloodBank entity in bloodBanks)
            {
                if (entity.Email.EmailAddress.Equals(bankEmail))
                {
                    if (!entity.ApiKey.Equals(bankApiKey)){
                        return false;
                    }
                }
            }
            return true;
        }
        public bool checkBloodBankExists(string bankEmail, List<BloodBank> bloodBanks)
        {
            foreach (BloodBank entity in bloodBanks)
            {
                if (entity.Email.EmailAddress.Equals(bankEmail))
                {
                    return true;
                }
            }
            return false;
        }

        
    }
}
