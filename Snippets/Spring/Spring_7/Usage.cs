﻿namespace Spring_7
{
    using NServiceBus;
    using Spring.Context.Support;

    class Usage
    {
        Usage(EndpointConfiguration endpointConfiguration)
        {
            #region Spring

            endpointConfiguration.UseContainer<SpringBuilder>();

            #endregion
        }

        void Existing(EndpointConfiguration endpointConfiguration)
        {
            #region Spring_Existing

            GenericApplicationContext applicationContext = new GenericApplicationContext();
            applicationContext.ObjectFactory.RegisterSingleton("MyService", new MyService());
            endpointConfiguration.UseContainer<SpringBuilder>(c => c.ExistingApplicationContext(applicationContext));

            #endregion
        }

        class MyService
        {
        }
    }
}