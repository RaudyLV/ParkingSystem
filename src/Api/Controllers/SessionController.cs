
using Application.Features.ParkingSession.Queries;
using Application.Features.ParkingSessions.Commands.CreateSessionCommands;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllSeccions()
        {
            return Ok(await Mediator.Send(new GetAllActiveSessionsQuery()));
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
        [ActionName("Entry")]
        public async Task<IActionResult> EntrySession([FromBody] CreateSessionCommand request)
        {
            return Ok(await Mediator.Send(new CreateSessionCommand
            {
                SlotId = request.SlotId,
                VehicleInfo = request.VehicleInfo,
                VehiclePlate = request.VehiclePlate
            }));
        }

       [HttpPut("{sessionId}")]
        public async Task<IActionResult> ExitSession(Guid sessionId, [FromBody] UpdateSessionCommand request)
        {
            request.SessionId = sessionId;
            return Ok(await Mediator.Send(request));
        }

    }
}