using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.Respositories;
using Shop.Services;

namespace Shop.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate ([FromBody]UserModel model )
        {
            UserRepository userRepository = new UserRepository();

            var user = userRepository.Get(model.UserName, model.Password);
  
            if (user == null)
                return NotFound(new {message = "Usuário ou Senha invalidos" });

            var token = TokenService.GenerationToken(user);

            user.Password = string.Empty;

            return new 
            { 
                user = user,
                token = token   
            };

        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => string.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee, manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";

    }
}
