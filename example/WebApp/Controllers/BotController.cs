using DomainLogic;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace WebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        [HttpPost("{token}")]
        public async Task<IActionResult> Post([FromServices] HandleUpdateService handleUpdateService,
                                          [FromBody] Update update,
                                          [FromRoute] string token)
        {
            await handleUpdateService.EchoAsync(update, token);
            return Ok();
        }
    }
}