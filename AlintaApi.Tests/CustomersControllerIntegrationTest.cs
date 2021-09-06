using System;
using System.Net;
using System.Text;
using System.Net.Http;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using AlintaApi.Domain.Models;
using System.Text.Json;

namespace AlintaApi.Tests
{
    [TestFixture]
    public class CustomersControllerIntegrationTest : IDisposable
    {
        private APIWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void GivenARequestToTheController()
        {
            _factory = new APIWebApplicationFactory<Startup>();
            _client = _factory.CreateClient();
        }

        private Customer ToCustomer(string firstName, string lastName, string dob, string id)
        {
            return new Customer() { 
                FirstName = firstName, 
                LastName = lastName, 
                DateOfBirth = DateTime.Parse(dob), 
                Id= new Guid(id)
            };
        }

        [Test, Order(1)]
        [TestCase("Mallik", "Alinta", "2000-04-6", "ef743a6d-e780-4406-9fd7-6e398db82adc")]
        [TestCase("Udaya", "Alinta", "2000-04-6", "6db0cc43-70ef-44d5-ac56-39970a036b3d")]
        [TestCase("Daksha", "Alinta", "2000-04-6", "753aca39-53f3-426c-b15e-c5385c38d3e4")]
        [TestCase("MyLittle", "Alinta", "2000-04-6", "49ff6071-49bc-4372-9c8d-26d190fa9485")]
        public async Task WhenAddingCustomer_ThenTheResultIsOk(string firstName, string lastName, string dob, string id)
        {
            var customer = ToCustomer(firstName, lastName, dob, id);

            var json = JsonSerializer.Serialize(customer);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await _client.PostAsync("api/Customers/CreateCustomer", httpContent);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }        

        [Test, Order(2)]
        public async Task WhenReadingAddedCustomer_ThenTheResultIsOk()
        {
            var textContent = new ByteArrayContent(Encoding.UTF8.GetBytes("Backpack for his applesauce"));
            textContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

            var result = await _client.GetAsync("api/Customers");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseContent = await result.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Length == 494);
        }

        [Test, Order(3)]
        [TestCase("Alinta")]        
        public async Task WhenGettingSelectiveCustomer_ThenTheResultIsOk(string name)
        {
            var json = JsonSerializer.Serialize(name);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await _client.GetAsync("api/Customers/"+ name);
            
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.NotNull(result.Content);

            var responseContent = await result.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Length == 494);
        }

        [Test, Order(4)]
        [TestCase("6db0cc43-70ef-44d5-ac56-39970a036b3d")]
        public async Task WhenDeleting_ThenTheResultIsOk(string guid)
        {
            var textContent = new ByteArrayContent(Encoding.UTF8.GetBytes("Backpack for his applesauce"));
            textContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

            var result = await _client.DeleteAsync("api/Customers/"+ guid);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseContent = await result.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Length == 121);
        }

        [Test, Order(5)]
        [TestCase("MallikUpdated", "Alinta", "2000-04-6", "ef743a6d-e780-4406-9fd7-6e398db82adc")]
        public async Task WhenUpdating_ThenTheResultIsOk(string firstName, string lastName, string dob, string id)
        {
            var customer = ToCustomer(firstName, lastName, dob, id);

            var json = JsonSerializer.Serialize(customer);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await _client.PutAsync("api/Customers/UpdateCustomer", httpContent);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseContent = await result.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Length == 129);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
