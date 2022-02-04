using Microsoft.AspNetCore.Mvc;
using RoomReservationWeb.Interface;
using RoomReservationWeb.Models;

namespace RoomReservationWeb.Controllers;
public class ReservationController : Controller
{
    private readonly IReservation _reservation;

    public ReservationController(IReservation reservation)
    {
        _reservation = reservation;
    }

    public async Task<IActionResult> Index()
    {
        //IEnumerable<TblTReservasi> listReservation = _reservation.GetReservations().Result;
        return View(await _reservation.GetReservations());
    }
}

