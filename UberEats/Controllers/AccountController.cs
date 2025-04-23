using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.Services;
using UberEats.Core.Application.ViewModels.User;

namespace WebApi.UberEats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly UserViewModel _userVm;
        public AccountController(ILoginService loginService)
        {
            _loginService = loginService;
            _userVm = new UserViewModel();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _loginService.LoginAsync(model);
            if (!result.IsSuccess)
                return Unauthorized(new { message = result.Message });

            return Ok(new { token = result.Token, userVm = result.UserVm});
        }
    }
}
