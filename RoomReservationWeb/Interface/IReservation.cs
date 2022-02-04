using RoomReservationWeb.Models;

namespace RoomReservationWeb.Interface;

public interface IReservation
{
    Task<IEnumerable<TblTReservasi>> GetReservations();
}
