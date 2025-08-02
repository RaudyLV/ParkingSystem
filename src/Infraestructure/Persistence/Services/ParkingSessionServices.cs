using Application.Dtos;
using Application.Exceptions;
using Application.Interfaces;
using Application.Spec.ParkingSessions;
using Domain.Entidades;

namespace Infraestructure.Persistence.Services
{
    public class ParkingSessionServices : IParkingSessionServices
    {
        private readonly IRepositoryAsync<ParkingSession> _repositoryAsync;
        public ParkingSessionServices(IRepositoryAsync<ParkingSession> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }
        public async Task StartSessionAsync(Guid slotId, string vehicleInfo, string vehiclePlate)
        {
            var existingSession = await _repositoryAsync.FirstOrDefaultAsync(new GetSessionByVehiclePlateSpec(vehiclePlate));

            if (existingSession != null)
            {
                throw new SlotOccupiedException($"Ya existe una sesión de estacionamiento activa para la placa {vehiclePlate}");
            }
           
            await _repositoryAsync.BeginTransactionAsync(); 
            try
            {
                var parkingSession = new ParkingSession
                {
                    ParkingSlotId = slotId,
                    VehiclePlate = vehiclePlate,
                    VehicleInfo = vehicleInfo,
                    StartTime = DateTime.UtcNow,
                    IsActive = true
                };

                await _repositoryAsync.AddAsync(parkingSession);
                await _repositoryAsync.SaveChangesAsync();

                await _repositoryAsync.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _repositoryAsync.RollbackTransactionAsync();
                throw new Exception("Error al iniciar la sesión de estacionamiento", ex);
            }
       
        }
        public async Task EndSessionAsync(Guid seccionId)
        {
           
            var activeSession = await GetActiveSessionAsync(seccionId); //ULTIMA SESSION ACTIVA Y SIN SALIDA

            //EJECUTA UN STORE PROCEDURE EN LA BD EL CUAL CALCULA EL MONTO TOTAL A PAGAR X LA CANTIDAD
            //DE TIEMPO QUE DURO ESTACIONADO EL VEHICULO EN EL SLOT, APARTE DE ACTUALIZAR EL ESTADO DE LA SESSION
            await _repositoryAsync.ExecuteRawSqlAsync("EXEC sp_CloseSession @p0", activeSession.Id);

            activeSession = await GetSessionByIdAsync(seccionId); //REFRESCAMOS LA ENTIDAD PARA GUARDAR CORRECTAMENTE
                                                                  //EL ESTADO DE LOS DATOS DEL STORE PROCEDURE
            
            await _repositoryAsync.UpdateAsync(activeSession);
            await _repositoryAsync.SaveChangesAsync();
        }

        public async Task<List<ParkingSessionDto>> GetActiveSessionsAsync()
             => await _repositoryAsync.ListAsync(new GetAllSessionsSpec());

        public async Task<ParkingSessionDto> GetSessionByVehiclePlateAsync(string vehiclePlate)
        {
            var session = await _repositoryAsync.FirstOrDefaultAsync(new GetSessionByVehiclePlateSpec(vehiclePlate));

            if (session == null)
            {
                throw new NotFoundException($"No se encontró la sesión de estacionamiento para la placa {vehiclePlate}");
            }

            return session;
        }

        public async Task<ParkingSession> GetActiveSessionAsync(Guid SlotId)
        {
            var activeSession = await _repositoryAsync.FirstOrDefaultAsync(new GetActiveSessionSpec(SlotId));
            if (activeSession == null)
            {
                throw new NotFoundException($"No se encontró una sesión activa para el espacio de estacionamiento {SlotId}");
            }

            return activeSession;
        }

        public async Task<ParkingSession> GetSessionByIdAsync(Guid seccionId)
        {
            var session = await _repositoryAsync.GetByIdAsync(seccionId);
            if (session is null)
            {
                throw new NotFoundException($"No se encontro una session con ID -{seccionId}");
            }

            return session;
        }
    }
}