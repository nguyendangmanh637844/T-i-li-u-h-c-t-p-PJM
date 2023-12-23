using Microsoft.AspNetCore.Mvc;
using Server_side.IServices;
using Server_side.models;

namespace Server_side.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignalRController : ControllerBase
    {
        private readonly ISignalRService _signalRService;

        public SignalRController(ISignalRService signalRService)
        {
            _signalRService = signalRService;
        }

        [HttpPost("sendToAll")]
        public async Task<IActionResult> SendToAll([FromBody] SendToAllRequest messageDto)
        {
            await _signalRService.SendToAll(messageDto);
            return Ok();
        }
    }
}