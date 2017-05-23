using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ContactService.Dto;
using ContactService.Interfaces;
using Newtonsoft.Json;

namespace ContactService.Services
{
    public class ContactService : IContactService
    {
        private static readonly HttpClient Client = new HttpClient();

        public async Task<HttpStatusCode> Add(Contact contact)
        {
            
            try
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(contact));

                HttpResponseMessage response = await Client.PostAsync("http://www.example.com/recepticle.aspx", stringContent);
                response.EnsureSuccessStatusCode();

                return response.StatusCode;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
