# DotNetCore.RabbitMQ

## Connection

```cs
public sealed record Connection(string HostName, int Port, string UserName, string Password);
```

## IQueue<T>

```cs
public interface IQueue<T>
{
    Task PublishAsync(T obj);

    Task SubscribeAsync(Func<T, Task> action);
}
```

## Queue<T>

```cs
public abstract class Queue<T> : IQueue<T>
{
    protected Queue(Connection connection) { }

    public async Task PublishAsync(T obj) { }

    public async Task SubscribeAsync(Func<T, Task> action) { }
}
```

## Example

### Message

```cs
public sealed record Product(string Name);
```

### Queue

```cs
public interface IProductQueue : IQueue<Product> { }
```

```cs
public class ProductQueue : Queue<Product>, IProductQueue
{
    public ProductQueue(Connection connection) : base(connection) { }
}
```

### Publisher

```cs
var product = new Product("Product");

IProductQueue productQueue = new ProductQueue(new Connection("localhost", 5672, "admin", "P4ssW0rd!"));

productQueue.PublishAsync(product).Wait();
```

### Subscriber

```cs
IProductQueue productQueue = new ProductQueue(new Connection("localhost", 5672, "admin", "P4ssW0rd!"));

productQueue.SubscribeAsync(product => Handle(product)).Wait();
```
