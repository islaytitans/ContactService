using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Dto
{
    public class Person
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_-]+(?:\.[a-zA-Z0-9_-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }

    }
}
