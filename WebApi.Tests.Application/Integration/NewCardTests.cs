using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RadiSoftware;
using WebApi.Application.Command;
using Xunit;

namespace WebApi.Tests.Integration
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    [Collection(nameof(IntergrationApiTestsFixtureCollection))]
    public class NewCardTests
    {
        private readonly IntegrationTestsFixtures<StatupApiTests> _testFixture;

        public NewCardTests(IntegrationTestsFixtures<StatupApiTests> testFixture)
        {
            _testFixture = testFixture;
        }

        [Fact]
        [Trait("Card", "Integrattion API - WebApi")]
        public async Task Create_New_Card_With_Sucess()
        {
            //Arrange
            var tokenRequest = new CardRequest()
            {
                CardNumber = 9999999999999999,
                CustomerId = 9,
                Cvv = 999
            };

            //Act
            string jsonString = JsonSerializer.Serialize(tokenRequest);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonString);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var postResponse = await _testFixture.Client.PostAsync("api/Card", byteContent);

            //Assert
            postResponse.EnsureSuccessStatusCode();
        }
    }
}
