using System;
using System.Collections.Generic;
using System.Net;
using ContactService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        public PersonController()
        {
            
        }

        [AllowAnonymous]
        [HttpGet]
        public string Index() => "PersonController";

        [AllowAnonymous]
        [HttpPost]
        public JsonResult Add([FromBody]IEnumerable<Person> persons)
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
        }
    }
}
