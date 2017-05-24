using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactService.Dto;
using ContactService.Interfaces;

namespace ContactService.Factories
{
    public class ContactFactory : IContactFactory
    {
        private readonly IContactBuilder _contactBuilder;

        public ContactFactory(IContactBuilder contactBuilder)
        {
            _contactBuilder = contactBuilder;
        }

        public IEnumerable<Contact> Manufacture(IEnumerable<Person> persons)
        {
            foreach (var person in persons)
            {
                Contact contact;

                try
                {
                    contact = _contactBuilder.Assemble(person);
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e);
                    continue;
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine(e);
                    continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                yield return contact;
            }
        }
    }
}
