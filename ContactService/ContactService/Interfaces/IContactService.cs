﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ContactService.Dto;

namespace ContactService.Interfaces
{
    public interface IContactService
    {
        Task<string> Add(string endPoint, Contact contact);
    }
}
