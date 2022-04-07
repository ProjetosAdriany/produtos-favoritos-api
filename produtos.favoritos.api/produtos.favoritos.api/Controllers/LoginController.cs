﻿using Domain.DTOs;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        //FromService = Injeção de dependência
        public async Task<object> Login([FromBody] LoginDTO login, [FromServices] ILoginService service)
        {
            if (!ModelState.IsValid || login == null)
                return BadRequest();

            try
            {
                var result = await service.FindByEmail(login);
                if (result == null)
                    return NotFound();

                return Ok(result);

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
