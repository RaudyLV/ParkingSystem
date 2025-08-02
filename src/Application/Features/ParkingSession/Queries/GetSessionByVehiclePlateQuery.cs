
using Application.Dtos;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;

namespace Application.Features.ParkingSession.Queries
{
    public class GetSessionByVehiclePlateQuery : IRequest<Response<ParkingSessionDto>>
    {
        public string vehiclePlate { get; set; } = string.Empty;
    }

    public class GetSessionByVehiclePlateQueryHandler: IRequestHandler<GetSessionByVehiclePlateQuery, Response<ParkingSessionDto>>
    {
        private readonly IParkingSessionServices _sessionServices;

        public GetSessionByVehiclePlateQueryHandler(IParkingSessionServices sessionServices)
        {
            _sessionServices = sessionServices;
        }

        public async Task<Response<ParkingSessionDto>> Handle(GetSessionByVehiclePlateQuery request, CancellationToken cancellationToken)
        {
            var session = await _sessionServices.GetSessionByVehiclePlateAsync(request.vehiclePlate);

            if (session is null)
            {
                throw new NotFoundException($"No se encontro session del vehiculo con matricula {request.vehiclePlate}");
            }

            return new Response<ParkingSessionDto>(session);
        }
    }
}