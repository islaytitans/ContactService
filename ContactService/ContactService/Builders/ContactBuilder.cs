using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactService.Dto;
using ContactService.Interfaces;

namespace ContactService.Builders
{
    public class ContactBuilder : IContactBuilder
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
                Gender = ParseGender(person.Gender),
                SamplesOrdered = ParseSampleOrders(person.Samples)
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

        private IEnumerable<KeyValuePair<string, string>> ParseSampleOrders(string samples)
        {
            string[] samplesCollection = samples.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

            return (from s in samplesCollection
                    let sample = s.Trim().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                    where sample.Length == 2
                    select new KeyValuePair<string, string>(sample[0], sample[1]));
        }
    }
}
