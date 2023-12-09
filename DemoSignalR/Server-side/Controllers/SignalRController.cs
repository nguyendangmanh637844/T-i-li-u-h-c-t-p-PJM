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
        public async Task<IActionResult> SendToAll([FromBody] MessageDto messageDto)
        {
            await _signalRService.SendToAll("ReceiveMessage", messageDto.User, messageDto.Message);
            return Ok();
        }

        [HttpPost("sendToUser")]
        public async Task<IActionResult> SendToUser([FromBody] MessageDto messageDto)
        {
            var connectionId = await _signalRService.GetConnectionIdByUsername(messageDto.User);
            if (connectionId != null)
            {
                await _signalRService.SendToConnection(connectionId, "ReceiveMessage", messageDto.User, messageDto.Message);
                return Ok();
            }

            return NotFound($"User '{messageDto.User}' not found or not connected.");
        }
    }
}