using System;
using System.Text;
using Models.DataModels;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace BookingWebsite.Services
{
    public class BookingService : IBookingService
    {

        private readonly IConnectionFactory _channelFactory;

        public BookingService()
        {
            _channelFactory = new ConnectionFactory() { HostName = "localhost" };
        }

        private bool SendBookingMessage(BookingChannelMessage channelMessage)
        {
            using (var connection = _channelFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "bookingChannel",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    string message = JsonConvert.SerializeObject(channelMessage);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                        routingKey: "bookingChannel",
                        basicProperties: null,
                        body: body);
                    Console.WriteLine(" [x] Sent {0}", message);

                }
            }
            return true;
        }

        public void CreateBooking(string name, string email, int bookingId, bool operation)
        {
            var result = new BookingChannelMessage(name, email, bookingId, operation);
            SendBookingMessage(result);
        }

        public void DeleteBooking(int bookingId, bool operation)
        {
            var result = new BookingChannelMessage(bookingId, operation);
            SendBookingMessage(result);
        }
    }
}
