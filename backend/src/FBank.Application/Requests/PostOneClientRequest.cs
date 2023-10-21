﻿using MediatR;

namespace FBank.Application.Requests
{
    public class PostOneClientRequest : IRequest<string>
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Password { get; set; }
    }
}
