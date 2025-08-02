
using Application.Features.ParkingSlot.Queries;
using Application.Features.ParkingSlots.Commands.CreateSlotsCommand;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SlotController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetSlots()
        {
            return Ok(await Mediator.Send(new GetAllSlotsQuery()));
        }
        [HttpGet("{vehicleType}")]
        public async Task<IActionResult> GetSlotsByVehicleType(string vehicleType)
        {
            return Ok(await Mediator.Send(new GetSlotsByVehicleTypeQuery{VehicleType = vehicleType}));
        }
        [HttpPost]
        public async Task<IActionResult> CreateSlot([FromBody] CreateSlotCommand slot)
        {
            return Ok(await Mediator.Send(new CreateSlotCommand
            {
                VehicleType = slot.VehicleType,
                LocationCode = slot.LocationCode,
            }));
        }
    }
}