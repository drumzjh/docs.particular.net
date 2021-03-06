﻿// ReSharper disable SuggestVarOrType_Elsewhere
namespace ASQ_7
{
    using NServiceBus;

    class Usage
    {
        void UseTransport(EndpointConfiguration endpointConfiguration)
        {
            #region AzureStorageQueueTransportWithAzure

            endpointConfiguration.UseTransport<AzureStorageQueueTransport>();

            #endregion
        }

        void CodeOnly(EndpointConfiguration endpointConfiguration)
        {
            #region AzureStorageQueueConfigCodeOnly

            var transport = endpointConfiguration.UseTransport<AzureStorageQueueTransport>();
            transport.ConnectionString("azure-storage-connection-string");
            transport.BatchSize(20);
            transport.MaximumWaitTimeWhenIdle(1000);
            transport.DegreeOfReceiveParallelism(16);
            transport.PeekInterval(100);

            #endregion
        }

        void AccountNamesInsteadOfConnectionStrings(EndpointConfiguration endpointConfiguration)
        {
            #region AzureStorageQueueUseAccountNamesInsteadOfConnectionStrings

            var transport = endpointConfiguration.UseTransport<AzureStorageQueueTransport>();
            transport.Addressing()
                .UseAccountNamesInsteadOfConnectionStrings();

            #endregion
        }

        #region AzureStorageQueueTransportWithAzureHost

        public class EndpointConfig : IConfigureThisEndpoint
        {
            public void Customize(EndpointConfiguration endpointConfiguration)
            {
                endpointConfiguration.UseTransport<AzureStorageQueueTransport>();
            }
        }

        #endregion
    }
}