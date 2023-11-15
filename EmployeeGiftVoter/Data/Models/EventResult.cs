using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class EventResult
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int GiftId { get; set; }
        public Gift Gift { get; set; }

        public int VoterId { get; set; }
        public Employee Voter { get; set; }
    }
}
