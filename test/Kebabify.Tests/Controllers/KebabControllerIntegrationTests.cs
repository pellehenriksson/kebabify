//using System;
//using System.Diagnostics.CodeAnalysis;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using Kebabify.Common;
//using Kebabify.Domain.Commands;
//using Kebabify.Domain.Models;
//using Newtonsoft.Json;
//using Xunit;

//namespace Kebabify.Tests.Controllers
//{
//    [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "This is fine for tests")]
//    public class KebabControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
//    {
//        private readonly CustomWebApplicationFactory<Startup> factory;

//        public KebabControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
//        {
//            this.factory = factory;
//        }

//        [Fact]
//        public async Task Get_Should_Return_Response()
//        {
//            var client = this.factory.CreateClient();
//            var response = await client.GetAsync(new Uri(@"/api/kebab", UriKind.Relative));

//            response.EnsureSuccessStatusCode();
//            response.Dispose();
//        }

//        [Fact(Skip = "requires emulator")]
//        public async Task Post_With_Message_Should_Return_Response()
//        {
//            var client = this.factory.CreateClient();

//            var body = new MakeKebab.Command
//            {
//                Input = "Hej alla barn!"
//            };

//            var message = new HttpRequestMessage(HttpMethod.Post, new Uri($@"/api/kebab", UriKind.Relative))
//            {
//                Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
//            };

//            var response = await client.SendAsync(message);

//            var result = await response.Content.ReadAsStringAsync();
//            var expected = new KebabModel { Kebab = "hej-alla-barn", Input = "Hej alla barn!", Started = new DateTime(2019, 1, 1), Completed = new DateTime(2019, 1, 1) }.ToJson();

//            response.EnsureSuccessStatusCode();
//            Assert.Equal(expected, result);

//            response.Dispose();
//            message.Dispose();
//        }
//    }
//}
