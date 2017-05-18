using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactService.Dto;
using ContactService.Interfaces;

namespace ContactService.Builders
{
    public class ContactsBuilder : IBuilder
    {
        public Contact Assemble(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            var contact = new Contact()
            {
                Email = person.Email,
                FirstName = person.FirstName,
                LastName = person.Surname,
                Gender = ParseGender(person.Gender)
            };

            return contact;
        }

        private string ParseGender(string gender)
        {
            switch (gender.ToLower())
            {
                case "0": 
                case "m":
                case "male":
                    return "Male";
                case "1":
                case "f":
                case "female":
                    return "Female";
                default:
                    throw new InvalidCastException();
            }
        }
    }
}
