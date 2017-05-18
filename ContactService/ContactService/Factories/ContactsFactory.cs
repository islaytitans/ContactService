using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactService.Interfaces;

namespace ContactService.Factories
{
    public class ContactsFactory : IFactory
    {
        private readonly IBuilder _builder;

        public ContactsFactory(IBuilder builder)
        {
            _builder = builder;
        }
    }
}
