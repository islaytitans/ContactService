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

namespace ContactService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly IContactFactory _contactFactory;
        private readonly IContactService _contactService;

        public PersonController(IContactFactory contactFactory, IContactService contactService)
        {
            _contactFactory = contactFactory;
            _contactService = contactService;
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

            var results = new List<KeyValuePair<string, HttpStatusCode>>();
            foreach (var contact in contacts)
            {
                results.Add(new KeyValuePair<string, HttpStatusCode>(contact.Email, await _contactService.Add(contact)));
            }

            // TODO status code
            return new JsonResult(results);
        }
    }
}
