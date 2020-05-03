using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TicketScannerBackend.Data;
using TicketScannerBackend.Models;

namespace TicketScannerBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private IAuthServices authServices;
        private IConfiguration configuration;
        public AuthController(IAuthServices authServices, IConfiguration configuration)
        {
            this.authServices = authServices;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]ClientCreds clientCreds)
        {
            var validation = ValidateCreds(clientCreds);
            if (validation.Error)
            {
                return BadRequest(CreateAuthResponse(true,validation.Message));
            }

            try
            {
                var client = await authServices.ClientAuth(clientCreds);
                if (client == null)
                {
                    return Unauthorized();
                }
                else
                {
                    var authResponse = CreateAuthResponse(false,"Valid");
                    authResponse.Client = client;
                    authResponse.Token = client.CreateJwtToken(configuration);
                    return Ok(authResponse);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, CreateAuthResponse(true,e.Message));
            }
        }

        [HttpPost("register")]
        public void Register([FromBody]ClientCreds clientCreds)
        {
        }

        private AuthResponse CreateAuthResponse(bool error,string message){
            return new AuthResponse(){
                Error = error,
                Message = message
            };
        }
        private CredsValidator ValidateCreds(ClientCreds clientCreds)
        {
            var credsValidator = new CredsValidator()
            {
                Error = false,
                Message = "Valid"
            };

            if (credsValidator == null)
            {
                credsValidator.Error = true;
                credsValidator.Message = "Creds null";
            }

            if (clientCreds.ClientName == null || clientCreds.ClientName.Equals(""))
            {
                credsValidator.Error = true;
                credsValidator.Message = "Client name null or empty";
            }

            if (clientCreds.Password == null || clientCreds.Password.Equals(""))
            {
                credsValidator.Error = true;
                credsValidator.Message = "Client password null or empty";
            }

            return credsValidator;
        }

        private class CredsValidator
        {
            public bool Error { get; set; }
            public string Message { get; set; }
        }
    }
}