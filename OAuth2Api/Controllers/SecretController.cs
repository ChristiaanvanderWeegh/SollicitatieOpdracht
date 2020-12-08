﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OAuth2Api.Controllers
{
    public class SecretController : Controller
    {
        [Authorize]
        public string Index()
        {
            return "Some secret message via OAuth2Api only accessable with an access token";
        }
    }
}
