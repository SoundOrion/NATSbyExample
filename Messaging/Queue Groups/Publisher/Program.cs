// Publisher.cs
using NATS.Net;

// `NATS_URL` environment variable can be used to pass the locations of the NATS servers.
var url = Environment.GetEnvironmentVariable("NATS_URL") ?? "nats://127.0.0.1:4222";

// Connect to NATS server.
await using var nc = new NatsClient(url);

// Publish a few orders.
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"Publishing order {i}...");
    await nc.PublishAsync($"orders.new.{i}", new Order(OrderId: i));
    await Task.Delay(500); // Simulate delay between publishing
}

Console.WriteLine("All orders published!");

// Order.cs
public record Order(int OrderId);
