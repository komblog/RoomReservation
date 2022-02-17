using Microsoft.EntityFrameworkCore;
using RoomReservationWeb.Dto;
using RoomReservationWeb.Interface;
using RoomReservationWeb.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace RoomReservationWeb.Repository;

public class ReservationRepository : IReservation
{
    private MDKAReservasiContext _context;
    public ReservationRepository(MDKAReservasiContext context)
    {
        _context = context;
    }
    public async Task<List<ListReservationDto>> GetAllReservations()
    {
        var listReservation = await _context.TblTReservasis.ToListAsync();

        var listReservationDtos = listReservation.Select(x => new ListReservationDto
        {
            IdReservasi = x.ReservasiPk,
            NamaRuangan = _context.TblMRuangans.Find(x.RuanganFk!).NamaRuangan!.ToString(),
            IdRuangan = x.RuanganFk.Value,
            SubjectReservasi = x.SubjectReservasi,
            TanggalReservasi = x.TanggalReservasi.Value,
            JamMulai = x.JamMulai,
            JamSelesai = x.JamSelesai,
            CreatedBy = x.CreatedBy,
            CreatedDate = x.CreatedDate
        });

        return listReservationDtos.ToList();
    }

    public async Task<ListReservationDto> GetReservations(int reservationId)
    {
        var getReservation = await _context.TblTReservasis.FindAsync(reservationId);
        var result = new ListReservationDto
        {
            IdReservasi = getReservation.ReservasiPk,
            IdRuangan = getReservation.RuanganFk.Value,
            NamaRuangan = _context.TblMRuangans.Find(getReservation.RuanganFk!).NamaRuangan!.ToString(),
            SubjectReservasi = getReservation.SubjectReservasi,
            TanggalReservasi = getReservation.TanggalReservasi,
            JamMulai = getReservation.JamMulai,
            JamSelesai = getReservation.JamSelesai
        };

        return result;

    }

    public async Task<List<DropDownListRoomDto>> GetDropDownListRooms()
    {
        var listRoom = await _context.TblMRuangans.Where(x => x.StatusFk == 2).ToListAsync();

        var listRoomDto = listRoom.Select(x => new DropDownListRoomDto
        {
            Id = x.RuanganPk,
            RoomName = x.NamaRuangan!
        });

        return listRoomDto.ToList();
    }

    public async Task<DropDownListRoomDto> GetRoom(int roomID)
    {
        var room = await _context.TblMRuangans.FindAsync(roomID);
        var roomDto = new DropDownListRoomDto
        {
            Id = room.RuanganPk,
            RoomName = room.NamaRuangan
        };

        return roomDto;
    }

    public async Task SaveReservation(ListReservationDto listReservationDto)
    {
        var newListReservation = new TblTReservasi
        {
            RuanganFk = listReservationDto.IdRuangan,
            SubjectReservasi = listReservationDto.SubjectReservasi,
            TanggalReservasi = listReservationDto.TanggalReservasi,
            JamMulai = listReservationDto.JamMulai,
            JamSelesai = listReservationDto.JamSelesai,
            CreatedBy = "System",
            CreatedDate = DateTime.Now
        };

        try
        {
            _context.Add(newListReservation);
            if (_context.SaveChanges() > 0)
            {
                await UpdateRuangan(listReservationDto.IdRuangan, 1);
            }

        }
        catch (DbEntityValidationException ex)
        {
            foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
            {
                // Get entry

                DbEntityEntry entry = item.Entry;
                string entityTypeName = entry.Entity.GetType().Name;

                // Display or log error messages

                foreach (DbValidationError subItem in item.ValidationErrors)
                {
                    string message = string.Format("Error '{0}' occurred in {1} at {2}",
                             subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                    Console.WriteLine(message);
                }
            }
        }

    }

    public async Task UpdateReservation(ListReservationDto listReservationDto)
    {
        var getReservation = await _context.TblTReservasis.FindAsync(listReservationDto.IdReservasi);

        try
        {
            if (getReservation == null)
            {
                return;
            }
            else
            {
                getReservation.SubjectReservasi = listReservationDto.SubjectReservasi;
                getReservation.TanggalReservasi = listReservationDto.TanggalReservasi;
                getReservation.JamMulai = listReservationDto.JamMulai;
                getReservation.JamSelesai = listReservationDto.JamSelesai;
                getReservation.UpdatedBy = "System";
                getReservation.UpdatedDate = DateTime.Now;

                _context.Update(getReservation);
                await _context.SaveChangesAsync();
            }
        }
        catch (DbEntityValidationException ex)
        {
            foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
            {
                // Get entry

                DbEntityEntry entry = item.Entry;
                string entityTypeName = entry.Entity.GetType().Name;

                // Display or log error messages

                foreach (DbValidationError subItem in item.ValidationErrors)
                {
                    string message = string.Format("Error '{0}' occurred in {1} at {2}",
                             subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                    Console.WriteLine(message);
                }
            }
        }

    }

    public async Task UpdateRuangan(int IdRuangan, int status)
    {
        try
        {
            var ruangan = _context.TblMRuangans.FindAsync(IdRuangan).Result;

            ruangan.StatusFk = status;
            _context.Update(ruangan);
            await _context.SaveChangesAsync();

        }
        catch (DbEntityValidationException ex)
        {
            foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
            {
                // Get entry

                DbEntityEntry entry = item.Entry;
                string entityTypeName = entry.Entity.GetType().Name;

                // Display or log error messages

                foreach (DbValidationError subItem in item.ValidationErrors)
                {
                    string message = string.Format("Error '{0}' occurred in {1} at {2}",
                             subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                    Console.WriteLine(message);
                }
            }
        }
    }

    public async Task DeleteReservation(int IdReservation)
    {
        var reservation = _context.TblTReservasis.Find(IdReservation);
        _context.TblTReservasis.Remove(reservation);

        if (_context.SaveChanges() > 0)
        {
            await UpdateRuangan(reservation.RuanganFk.Value, 2);
        }

    }

}

