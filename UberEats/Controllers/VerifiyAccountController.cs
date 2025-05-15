using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UberEats.Core.Application.Interfaces.Services;

namespace WebApi.UberEats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifiyAccountController : ControllerBase
    {
        private readonly IVerifyAccountService _verifyAccountService;

        public VerifiyAccountController(IVerifyAccountService verifyAccountService)
        {
            _verifyAccountService = verifyAccountService;
        }

        [HttpDelete("verify")]

        public void verifyAccount(string email)
        {
            _verifyAccountService.VerifyAccount(email);
        }

        [HttpGet]

        public IActionResult GetAllUnverifiedAccounts()
        {
            var verifiyAccount = _verifyAccountService.GetUnverifiedAccounts();
            return Ok(verifiyAccount);
        }
    }
}
