
using Application.Features.ParkingSession.Queries;
using Application.Features.ParkingSessions.Commands.CreateSessionCommands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Operador")]
    public class SessionController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllSeccions()
        {
            var result = await Mediator.Send(new GetAllActiveSessionsQuery());

            if (result == null || !result.Data.Any())
                return NoContent(); 

            return Ok(result);
        }

        [HttpGet("{vehicleplate}")]
        public async Task<IActionResult> GetSessionByPlate(string vehicleplate)
        {
            return Ok(await Mediator.Send(new GetSessionByVehiclePlateQuery
            {
                vehiclePlate = vehicleplate
            }));
        }

        [HttpPost]
        public async Task<IActionResult> EntrySession([FromBody] CreateSessionCommand request)
        {
            var command = await Mediator.Send(request);

            if (!command.Succeeded)
                return BadRequest(command.Errors);

            return CreatedAtAction(nameof(GetAllSeccions), new { id = request.SlotId }, request);
        }

        [HttpPut("{sessionId}")]
        public async Task<IActionResult> ExitSession(Guid sessionId, [FromBody] UpdateSessionCommand request)
        {
            if (sessionId != request.SessionId)
                return BadRequest("Session ID en la URL no matchea el pasado en el query");

            var response = await Mediator.Send(request);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return NoContent();
        }

    }
}