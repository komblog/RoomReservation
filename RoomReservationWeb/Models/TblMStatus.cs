using System;
using System.Collections.Generic;

namespace RoomReservationWeb.Models
{
    public partial class TblMStatus
    {
        public TblMStatus()
        {
            TblMRuangans = new HashSet<TblMRuangan>();
        }

        public int StatusPk { get; set; }
        public string? NamaStatus { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<TblMRuangan> TblMRuangans { get; set; }
    }
}
