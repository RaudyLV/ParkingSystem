
using Application.Features.ParkingSlot.Queries;
using Application.Features.ParkingSlots.Commands.CreateSlotsCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Operador")]
    public class SlotController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetSlots()
        {
            var response = await Mediator.Send(new GetAllSlotsQuery());

            if (response is null || !response.Data.Any())
                return NoContent();

            return Ok(response);
        }
        [HttpGet("{vehicleType}")]
        public async Task<IActionResult> GetSlotsByVehicleType(string vehicleType)
        {
            return Ok(await Mediator.Send(new GetSlotsByVehicleTypeQuery { VehicleType = vehicleType }));
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSlot([FromBody] CreateSlotCommand request)
        {
            var response = await Mediator.Send(request);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return CreatedAtAction(nameof(GetSlots), new { id = request.LocationCode }, request);
        }
    }
}