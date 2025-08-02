
using Application.Dtos;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entidades;
using MediatR;


public class GetAllActiveSessionsQuery : IRequest<Response<List<ParkingSessionDto>>>;

public class GetAllActiveSessionsQueryHandler : IRequestHandler<GetAllActiveSessionsQuery, Response<List<ParkingSessionDto>>>
{

    private readonly IParkingSessionServices _sessionServices;

    public GetAllActiveSessionsQueryHandler(IParkingSessionServices sessionServices)
    {
        _sessionServices = sessionServices;
    }

    public async Task<Response<List<ParkingSessionDto>>> Handle(GetAllActiveSessionsQuery request, CancellationToken cancellationToken)
    {
        var sessions = await _sessionServices.GetActiveSessionsAsync();
        
        if (sessions.Count() < 1)
        {
            throw new NotFoundException("No hay sessiones activas");
        }

        return new Response<List<ParkingSessionDto>>(sessions);
    }
}


        
    
