using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TaxApp.Application.WebApi;

namespace TaxApp.IntegrationTest
{
    public class TestClientProvider
    {
        public HttpClient client { get; private set; }

        public TestClientProvider()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            client = server.CreateClient();
        }
    }
}
