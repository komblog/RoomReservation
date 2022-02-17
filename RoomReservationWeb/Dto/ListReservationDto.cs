using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoomReservationWeb.Dto;

public class ListReservationDto
{
    public int IdReservasi { get; set; }

    [Required]
    public int IdRuangan { get; set; }

    [DisplayName("Ruangan")]
    public string? NamaRuangan { get; set; }

    [DisplayName("Subject reservasi")]
    [Required]
    public string? SubjectReservasi { get; set; }

    [DisplayName("Tanggal reservasi")]
    [Required]
    public DateTime? TanggalReservasi { get; set; }

    [DisplayName("Jam mulai")]
    [Required]
    public TimeSpan? JamMulai { get; set; }

    [DisplayName("Jam selesai")]
    [Required]
    public TimeSpan? JamSelesai { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

