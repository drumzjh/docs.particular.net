using NServiceBus;

[EndpointName("Samples.Logging.HostProfiles")]
public class EndpointConfig :
    IConfigureThisEndpoint,
    AsA_Server,
    IWantCustomInitialization
{
    public void Init()
    {
        Configure configure = Configure.With();
        configure.DefaultBuilder();
        configure.InMemorySagaPersister();
        configure.RunTimeoutManagerWithInMemoryPersistence();
        configure.InMemorySubscriptionStorage();
        configure.JsonSerializer();
    }
}