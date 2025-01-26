// Subscriber.cs
using NATS.Net;

var url = Environment.GetEnvironmentVariable("NATS_URL") ?? "nats://127.0.0.1:4222";

// Connect to NATS server.
await using var nc = new NatsClient(url);

// Subscribe to a subject and start waiting for messages in the background.
Console.WriteLine("Waiting for messages...");
var cts = new CancellationTokenSource();
var subscriptionTask = Task.Run(async () =>
{
    await foreach (var msg in nc.SubscribeAsync<Order>("orders.>", cancellationToken: cts.Token))
    {
        var order = msg.Data;
        Console.WriteLine($"Subscriber received {msg.Subject}: {order}");
    }

    Console.WriteLine("Unsubscribed");
});

// Wait for cancellation to exit cleanly.
Console.CancelKeyPress += async (_, _) =>
{
    Console.WriteLine("Cancelling subscription...");
    await cts.CancelAsync();
    await subscriptionTask;
};

Console.WriteLine("Press Ctrl+C to stop.");
await Task.Delay(Timeout.Infinite); // Keep the program running

// Order.cs
public record Order(int OrderId);
