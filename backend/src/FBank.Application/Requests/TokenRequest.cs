﻿using FBank.Application.Interfaces;
using MediatR;

namespace FBank.Application.Requests
{
    public class TokenRequest : IRequest<string>, IPersistable
    {
        public int NumberAgency { get; set; }
        public int NumberAccount { get; set; }
        public string Password { get; set; }
    }
}
