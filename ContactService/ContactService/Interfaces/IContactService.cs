using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactService.Dto;

namespace ContactService.Interfaces
{
    public interface IContactService
    {
        Task Add(Contact contact);
    }
}
