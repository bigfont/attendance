using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Attendance.DataAccess.Models
{
    public class Visit
    {
        public int Id { get; set; }

        [Index("IX_PersonIdEventIdDateTime", IsUnique = true, Order = 0)]
        public int PersonId { get; set; }
        [Index("IX_PersonIdEventIdDateTime", IsUnique = true, Order = 1)]
        public int EventId { get; set; }
        [Index("IX_PersonIdEventIdDateTime", IsUnique = true, Order = 2)]
        public DateTime DateTime { get; set; }
        public virtual Person Person { get; set; }
        public virtual Event Event { get; set; }
    }
}