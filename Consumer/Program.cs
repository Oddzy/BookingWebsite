using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Models.DataModels;
using Newtonsoft.Json;

namespace Consumer
{
    class Program
    {

        public static List<Booking> DataBase { get; set; } = new();

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "bookingChannel",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var booking = JsonConvert.DeserializeObject<BookingChannelMessage>(message);

                    if (booking != null)
                    {
                        switch (booking.Operation)
                        {
                            case true:
                                Console.WriteLine("Added booking to database booking id : {0}, name:{1}", booking.BookingId, booking.Name);
                                var bookingToAdd = new Booking(booking.Name, booking.Email, booking.BookingId);
                                if (DataBase.Any(x => x.BookingId == bookingToAdd.BookingId))
                                {
                                    Console.WriteLine($"Cannot create booking, booking ID:{bookingToAdd.BookingId} already exists");
                                }
                                DataBase.Add(booking);
                                break;
                            case false:
                                var bookingToRemove = DataBase.First(x => x.BookingId == booking.BookingId);

                                if (bookingToRemove == null)
                                {
                                    Console.WriteLine($"No booking with ID:{booking.BookingId} was found");

                                }
                                else
                                {
                                    Console.WriteLine("Removed booking from database booking id : {0}, name:{1}", booking.BookingId, booking.Name);
                                    DataBase.Remove(bookingToRemove);

                                }
                                break;
                        }
                    }
                    Console.WriteLine(" [x] Received {0}", message);
                };

                channel.BasicConsume(queue: "bookingChannel",
                    autoAck: true,
                    consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
