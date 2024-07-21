namespace DotNetCore.RabbitMQ;

public interface IQueue<T>
{
    void Publish(T obj);

    void Subscribe(Action<T> action);
}
