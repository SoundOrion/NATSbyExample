// Publisher.cs
using System.Diagnostics;
using NATS.Client.Core;
using NATS.Net;

var stopwatch = Stopwatch.StartNew();

// `NATS_URL` environment variable can be used to pass the locations of the NATS servers.
var url = Environment.GetEnvironmentVariable("NATS_URL") ?? "127.0.0.1:4222";
Log($"[CON] Connecting to {url}...");

// Connect to NATS server.
await using var nc = new NatsClient(url);

// Request and receive replies
var replyOpts = new NatsSubOpts { Timeout = TimeSpan.FromSeconds(2) };

Log("[REQ] From joe");
var reply = await nc.RequestAsync<int, string>("greet.joe", 0, replyOpts: replyOpts);
Log($"[REQ] {reply.Data}");

Log("[REQ] From sue");
reply = await nc.RequestAsync<int, string>("greet.sue", 0, replyOpts: replyOpts);
Log($"[REQ] {reply.Data}");

Log("[REQ] From bob");
reply = await nc.RequestAsync<int, string>("greet.bob", 0, replyOpts: replyOpts);
Log($"[REQ] {reply.Data}");

try
{
    Log("[REQ] From joe (after responder is stopped)");
    reply = await nc.RequestAsync<int, string>("greet.joe", 0, replyOpts: replyOpts);
    Log($"[REQ] {reply.Data} - We should not see this message.");
}
catch (NatsNoRespondersException)
{
    Log("[REQ] No responders!");
}

Log("Publisher finished!");
await Task.Delay(Timeout.Infinite); // Keep the responder running
return;

void Log(string log) => Console.WriteLine($"{stopwatch.Elapsed} {log}");
