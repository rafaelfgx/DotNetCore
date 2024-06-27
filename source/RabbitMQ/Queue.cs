using DotNetCore.Extensions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DotNetCore.RabbitMQ;

public abstract class Queue<T> : IQueue<T>
{
    private readonly Connection _connection;

    private readonly string _queue = typeof(T).Name;

    protected Queue(Connection connection) => _connection = connection;

    public void Publish(T obj) => Connection(channel => channel.BasicPublish(string.Empty, _queue, default, obj.Bytes()));

    public void Subscribe(Action<T> action)
    {
        Connection(channel =>
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (_, args) => action(args.Body.ToArray().Object<T>());
            var autoResetEvent = new AutoResetEvent(false);
            channel.BasicConsume(_queue, true, consumer);
            Console.CancelKeyPress += (_, args) => { autoResetEvent.Set(); args.Cancel = true; };
            autoResetEvent.WaitOne();
        });
    }

    private void Connection(Action<IModel> action)
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = _connection.HostName,
            Port = _connection.Port,
            UserName = _connection.UserName,
            Password = _connection.Password
        };

        using var connection = connectionFactory.CreateConnection();

        using var channel = connection.CreateModel();

        channel.QueueDeclare(_queue, true, false);

        action(channel);
    }
}
