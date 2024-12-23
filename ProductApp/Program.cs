using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// https://www.youtube.com/watch?v=KhYiaEOrw7Q
// http://127.0.0.1:15672/
// User/Pw: guest
// https://www.rabbitmq.com/tutorials/tutorial-one-dotnet

Console.WriteLine("RabbitMQ Consumer!");

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false,
    arguments: null);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");
    return Task.CompletedTask;
};

await channel.BasicConsumeAsync("hello", autoAck: true, consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();