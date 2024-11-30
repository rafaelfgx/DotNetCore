namespace DotNetCore.RabbitMQ;

public interface IQueue<T>
{
    Task PublishAsync(T obj);

    Task SubscribeAsync(Func<T, Task> action);
}
