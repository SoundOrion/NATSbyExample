// Subscriber.cs
using System.Diagnostics;
using NATS.Client.Core;
using NATS.Net;

var stopwatch = Stopwatch.StartNew();

// `NATS_URL` environment variable can be used to pass the locations of the NATS servers.
var url = Environment.GetEnvironmentVariable("NATS_URL") ?? "127.0.0.1:4222";
Log($"[CON] Connecting to {url}...");

// Connect to NATS server.
await using var nc = new NatsClient(url);

var cts = new CancellationTokenSource();

var responder = Task.Run(async () =>
{
    // Subscribe to `greet.*` and handle requests
    await foreach (var msg in nc.SubscribeAsync<int>("greet.*").WithCancellation(cts.Token))
    {
        var name = msg.Subject.Split('.')[1];
        Log($"[REP] Received request: {msg.Subject}");
        await Task.Delay(500); // Simulate processing delay
        await msg.ReplyAsync($"Hello {name}!");
    }
});

Console.WriteLine("Responder running. Press Ctrl+C to stop.");
Console.CancelKeyPress += async (_, _) =>
{
    Log("Cancelling responder...");
    await cts.CancelAsync();
    await responder;
    Log("Responder stopped.");
};

await Task.Delay(Timeout.Infinite); // Keep the responder running
return;

void Log(string log) => Console.WriteLine($"{stopwatch.Elapsed} {log}");
