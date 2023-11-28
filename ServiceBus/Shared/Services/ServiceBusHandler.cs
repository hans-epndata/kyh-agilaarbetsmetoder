using Azure.Messaging.ServiceBus;

namespace Shared.Services;

public class ServiceBusHandler
{
    private readonly ServiceBusClient _client;
    private readonly ServiceBusSender _pub;
    private readonly ServiceBusProcessor _sub;

    public ServiceBusHandler(string connectionString, string topic, string subscription)
    {
        _client = new ServiceBusClient(connectionString);
        _pub = _client.CreateSender(topic);
        _sub = _client.CreateProcessor(topic, subscription, new ServiceBusProcessorOptions());

        _sub.ProcessMessageAsync += MessageHandler;
        _sub.ProcessErrorAsync += ErrorHandler;
    }

    public async Task SendMessageAsync(string content, string messageType)
    {
        var message = new ServiceBusMessage(content);
        message.ApplicationProperties.Add("messageType", messageType);
        await _pub.SendMessageAsync(message);
        Console.WriteLine($"Message sent to [{messageType}]: {message}");
    }

    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        string message = args.Message.Body.ToString();
        Console.WriteLine($"Message Received: {message}");

        await args.CompleteMessageAsync(args.Message);
    }

    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine($"Error: {args.Exception}");
        return Task.CompletedTask;
    }

    public async Task StartSubscribingAsync() => await _sub.StartProcessingAsync();
    public async Task StopSubscribingAsync() => await _sub.StopProcessingAsync();
}
