using Shared.Services;

namespace AuthenticationProvider.Services;

public class AuthService
{
    private readonly string _connectionString = "Endpoint=sb://kyh-servicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=2g+DZngdUtP6Nm1ZTfX3XijAaqha6Cw3x+ASbMYpa3A=";
    private readonly string _topic = "communication";
    private readonly string _subscription = "authenticationprovider";
    private readonly ServiceBusHandler _serviceBusHandler;

    public AuthService()
    {
        _serviceBusHandler = new ServiceBusHandler(_connectionString, _topic, _subscription);
        _serviceBusHandler.StartSubscribingAsync().ConfigureAwait(false);
    }

    public async Task SendVerificationEmailAsync(string email)
    {
        await _serviceBusHandler.SendMessageAsync(email, "mail");
    }
}
