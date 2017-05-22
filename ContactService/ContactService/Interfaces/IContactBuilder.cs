using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactService.Dto;

namespace ContactService.Interfaces
{
    public interface IContactBuilder
    {
        Contact Assemble(Person person);
    }
}
