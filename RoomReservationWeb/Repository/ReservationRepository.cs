using Microsoft.EntityFrameworkCore;
using RoomReservationWeb.Interface;
using RoomReservationWeb.Models;

namespace RoomReservationWeb.Repository;

public class ReservationRepository : IReservation
{
    private MDKAReservasiContext _context;
    public ReservationRepository(MDKAReservasiContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<TblTReservasi>> GetReservations()
    {
        return await _context.TblTReservasis.ToListAsync();
    }
}

