using Newtonsoft.Json;
using Shared.Services;

string connectionString = "Endpoint=sb://kyh-servicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=2g+DZngdUtP6Nm1ZTfX3XijAaqha6Cw3x+ASbMYpa3A=";
string topic = "communication";
string subscription = "orderprovider";

var serviceBus = new ServiceBusHandler(connectionString, topic, subscription);

// Börja lyssna på meddelanden
await serviceBus.StartSubscribingAsync();

while (true)
{
    Console.Clear();

    Console.Write("Till: ");
    string to = Console.ReadLine()!;

    Console.Write("Rubrik: ");
    string subject = Console.ReadLine()!;

    Console.Write("Meddelande: ");
    string message = Console.ReadLine()!;

    string json = JsonConvert.SerializeObject(new { to, subject, message });

    if (!string.IsNullOrEmpty(json))
    {
        await serviceBus.SendMessageAsync(json, "mail");
    }

    Console.WriteLine("Tryck på valfri knapp för att fortsätta.");
    Console.ReadKey();
}

await serviceBus.StopSubscribingAsync();