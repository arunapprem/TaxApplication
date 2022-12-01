using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace TaxApp.IntegrationTest
{
    public class TaxDetailsIntergrationTest
    {
        [Fact]
        public async Task Test_GetTaxDetails()
        {
            var client = new TestClientProvider().client;
            var response = await client.GetAsync("/api/values");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
