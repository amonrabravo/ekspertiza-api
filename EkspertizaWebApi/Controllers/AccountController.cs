using EkspertizaWebApi.Models;
using EkspertizaWebApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EkspertizaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(
            IAccountService accountService
            )
        {
            this.accountService = accountService;
        }

        [HttpPost("Regiser")]
        public async Task<IdentityResult> Register(RegisterViewModel model) => await accountService.RegisterAsync(model);

        [HttpPost("Token")]
        public async Task<TokenResult> Token(TokenViewModel model) => await accountService.TokenAsync(model);
    }
}
