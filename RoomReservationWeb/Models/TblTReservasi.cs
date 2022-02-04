using System;
using System.Collections.Generic;

namespace RoomReservationWeb.Models
{
    public partial class TblTReservasi
    {
        public int ReservasiPk { get; set; }
        public int? RuanganFk { get; set; }
        public string? SubjectReservasi { get; set; }
        public DateTime? TanggalReservasi { get; set; }
        public TimeSpan? JamMulai { get; set; }
        public TimeSpan? JamSelesai { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual TblMRuangan RuanganFkNavigation { get; set; }
    }
}
