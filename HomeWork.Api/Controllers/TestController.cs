using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            Thread.Sleep(2000);
            return Ok("pong ✅");
        }

        [HttpPost("secure")]
        public IActionResult Secure() => Ok("secure ✅");

        [HttpGet("error")]
        public IActionResult ThrowError() => throw new Exception("Тестовая ошибка");
    }
}
