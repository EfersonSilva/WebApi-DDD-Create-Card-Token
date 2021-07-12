using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using RadiSoftware;
using WebApi.Tests.Config;
using Xunit;

namespace WebApi.Tests.Integration
{
    [CollectionDefinition(nameof(IntergrationApiTestsFixtureCollection))]
    public class IntergrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixtures<StatupApiTests>>
    {
    }
    public class IntegrationTestsFixtures<TStartup> : IDisposable where TStartup : class
    {
        public readonly WebApiFactory<TStartup> Factory;
        public HttpClient Client;

        public IntegrationTestsFixtures()
        {
            Factory = new WebApiFactory<TStartup>();
            Client = Factory.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}
