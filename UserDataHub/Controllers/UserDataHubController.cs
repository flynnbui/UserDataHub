using Microsoft.AspNetCore.Mvc;
using UserDataHub.Core.DTOs;
using UserDataHub.Core.Interfaces;

namespace UserDataHub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserDataHubController : ControllerBase
    {
        private readonly RabbitMQService _rabbitMQService;

        public UserDataHubController(RabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UpdateUserInfoDto userData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _rabbitMQService.PublishAsync(userData);

            return Ok(userData);
        }
    }
}
