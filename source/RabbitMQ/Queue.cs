using DotNetCore.Extensions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DotNetCore.RabbitMQ;

public abstract class Queue<T>(Connection connection) : IQueue<T>
{
    private readonly ConnectionFactory _connectionFactory = new()
    {
        HostName = connection.HostName,
        Port = connection.Port,
        UserName = connection.UserName,
        Password = connection.Password
    };

    public async Task PublishAsync(T obj)
    {
        await using var connection = await _connectionFactory.CreateConnectionAsync();

        await using var channel = await connection.CreateChannelAsync();

        await QueueDeclareAsync(channel);

        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: typeof(T).Name, body: obj.Bytes());
    }

    public async Task SubscribeAsync(Func<T, Task> action)
    {
        await using var connection = await _connectionFactory.CreateConnectionAsync();

        await using var channel = await connection.CreateChannelAsync();

        await QueueDeclareAsync(channel);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += (_, args) => action(args.Body.ToArray().Object<T>());

        await channel.BasicConsumeAsync(queue: typeof(T).Name, autoAck: true, consumer: consumer);

        var autoResetEvent = new AutoResetEvent(false);

        Console.CancelKeyPress += (_, args) => { autoResetEvent.Set(); args.Cancel = true; };

        autoResetEvent.WaitOne();
    }

    private static async Task QueueDeclareAsync(IChannel channel)
    {
        await channel.QueueDeclareAsync(queue: typeof(T).Name, durable: true, exclusive: false, autoDelete: false);
    }
}
