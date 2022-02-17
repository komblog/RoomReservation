using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoomReservationWeb.Dto;
using RoomReservationWeb.Helper;
using RoomReservationWeb.Interface;

namespace RoomReservationWeb.Controllers;
public class ReservationController : Controller
{
    private readonly IReservation _reservation;

    public ReservationController(IReservation reservation)
    {
        _reservation = reservation;
    }

    // Get : Reservation/Index
    public ActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<ActionResult> Reservations()
    {
        var reservationList = await _reservation.GetAllReservations();
        return Json(new { data = reservationList });
    }

    // Get : Reservation/AddOrEdit
    [NoDirectAccess]
    public async Task<IActionResult> AddOrEdit(int IdReservasi = 0)
    {
        var roomDictionary = await _reservation.GetDropDownListRooms();

        if (IdReservasi == 0)
        {
            var roomList = new SelectList(roomDictionary, nameof(DropDownListRoomDto.Id), nameof(DropDownListRoomDto.RoomName));
            ViewBag.RoomList = roomList;
            return View(new ListReservationDto());
        }
        else
        {
            var getReservation = await _reservation.GetReservations(IdReservasi);
            var room = await _reservation.GetRoom(getReservation.IdRuangan);
            roomDictionary.Add(room);
            var roomList = new SelectList(roomDictionary, nameof(DropDownListRoomDto.Id), nameof(DropDownListRoomDto.RoomName));
            ViewBag.RoomList = roomList;

            if (getReservation == null)
            {
                return NotFound();
            }

            return View(getReservation);
        }

    }

    // Post : Reservation/AddOrEdit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrEdit([Bind("IdReservasi, IdRuangan, SubjectReservasi, JamMulai, JamSelesai, TanggalReservasi")] ListReservationDto listReservationDto)
    {
        var roomDictionary = await _reservation.GetDropDownListRooms();
        if (ModelState.IsValid)
        {
            if (listReservationDto.IdReservasi == 0)
            {
                var roomList = new SelectList(roomDictionary, nameof(DropDownListRoomDto.Id), nameof(DropDownListRoomDto.RoomName));
                ViewData["RoomList"] = roomList;
                //Insert operation
                await _reservation.SaveReservation(listReservationDto);
            }
            else
            {                
                try
                {
                    var getReservation = await _reservation.GetReservations(listReservationDto.IdReservasi);
                    var room = await _reservation.GetRoom(getReservation.IdRuangan);
                    roomDictionary.Add(room);
                    var roomList = new SelectList(roomDictionary, nameof(DropDownListRoomDto.Id), nameof(DropDownListRoomDto.RoomName));
                    ViewData["RoomList"] = roomList;

                    // Update operation
                    await _reservation.UpdateReservation(listReservationDto);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return Json(new { isValid = true });
            
        }

        return Json(new { isValid = false, html = RenderView.RenderRazorViewToString(this, "AddOrEdit", listReservationDto) });
        //return RedirectToAction("Index");
    }

    // Post : Reservation/Delete/1
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> Delete(int IdReservasi)
    {
        await _reservation.DeleteReservation(IdReservasi);
        return Json(new { message =  "Success"});
    }
}

