using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ContactService.Dto;
using ContactService.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ContactService.Services
{
    public class ContactService : IContactService
    {
        private static readonly HttpClient Client = new HttpClient();

        public async Task<string> Add(string endPoint, Contact contact)
        {
            try
            {
                Client.Timeout = TimeSpan.FromMilliseconds(10);

                var stringContent = new StringContent(JsonConvert.SerializeObject(contact));

                HttpResponseMessage response = await Client.PostAsync(endPoint, stringContent);

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
