using System;
using System.Collections.Generic;

namespace RoomReservationWeb.Models
{
    public partial class TblMRuangan
    {
        public int RuanganPk { get; set; }
        public string? NamaRuangan { get; set; }
        public int? Lantai { get; set; }
        public int? DayaTampung { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? StatusFk { get; set; }
    }
}
