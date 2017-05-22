using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ContactService.Dto;
using ContactService.Interfaces;

namespace ContactService.Services
{
    public class ContactService : IContactService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task Add(Contact contact)
        {
            var values = new Dictionary<string, string>
            {
                { "thing1", "hello" },
                { "thing2", "world" }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://www.example.com/recepticle.aspx", content);

            var responseString = await response.Content.ReadAsStringAsync();
        }
    }
}
