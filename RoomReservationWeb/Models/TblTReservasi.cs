using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RoomReservationWeb.Models
{
    public partial class TblTReservasi
    {
        public int ReservasiPk { get; set; }

        [DisplayName("Ruangan")]
        public int? RuanganFk { get; set; }

        [DisplayName("Subject reservasi")]
        public string? SubjectReservasi { get; set; }

        [DisplayName("Tanggal reservasi")]
        public DateTime? TanggalReservasi { get; set; }

        [DisplayName("Jam mulai")]
        public TimeSpan? JamMulai { get; set; }

        [DisplayName("Jam selesai")]
        public TimeSpan? JamSelesai { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
