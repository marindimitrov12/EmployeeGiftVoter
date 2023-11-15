using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public int InitiatorId { get; set; }
        public Employee Initiator { get; set; }
        public int BirthdayBoyId { get; set; }
        public Employee BirthdayBoy { get; set; }
        public List<EventResult>Results{get;set;}=new List<EventResult>();
    }
}
