using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ContactService.Dto;
using ContactService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ContactService.Controllers
{
    [Authorize]
    [Route("contactservice/[action]")]
    public class PersonController : Controller
    {
        private readonly IContactFactory _contactFactory;
        private readonly IContactService _contactService;
        private readonly IConfigurationRoot _configuration;

        public PersonController(IContactFactory contactFactory, IContactService contactService, IConfigurationRoot configuration)
        {
            _contactFactory = contactFactory;
            _contactService = contactService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet]
        public string Index() => "PersonController";

        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> Add([FromBody]IEnumerable<Person> persons)
        {
            if (persons == null)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json(new ArgumentNullException($"{nameof(persons)} was not passed"));
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new ArgumentException($"{nameof(persons)} was not valid"));
            }

            var contacts = _contactFactory.Manufacture(persons);

            var taskList = new List<Task<string>>();

            string contactsDestinationUrl = _configuration["ContactDestinationUrl"];

            foreach (var contact in contacts)
            {
                taskList.Add(_contactService.Add(contactsDestinationUrl, contact));
            }

            try
            {
                await Task.WhenAll(taskList.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(e.Message);
            }

            Response.StatusCode = (int) HttpStatusCode.OK;
            return new JsonResult(taskList);
        }
    }
}
