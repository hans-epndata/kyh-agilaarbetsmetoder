using Shared.Services;

string connectionString = "Endpoint=sb://kyh-servicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=2g+DZngdUtP6Nm1ZTfX3XijAaqha6Cw3x+ASbMYpa3A=";
string topic = "communication";
string subscription = "mailprovider";

var serviceBus = new ServiceBusHandler(connectionString, topic, subscription);

await serviceBus.StartSubscribingAsync();

Console.WriteLine("Tryck på valfri tangent för att avsluta.");
Console.ReadKey();

await serviceBus.StopSubscribingAsync();