using RoomReservationWeb.Dto;
using RoomReservationWeb.Models;

namespace RoomReservationWeb.Interface;

public interface IReservation
{
    Task<List<ListReservationDto>> GetAllReservations();
    Task<ListReservationDto> GetReservations(int reservationId);
    Task<List<DropDownListRoomDto>> GetDropDownListRooms();
    Task SaveReservation(ListReservationDto listReservationDto);
    Task UpdateReservation(ListReservationDto listReservationDto);
    Task UpdateRuangan(int IdRuangan, int status);
    Task<DropDownListRoomDto> GetRoom(int roomID);
    Task DeleteReservation(int IdReservation);

}
