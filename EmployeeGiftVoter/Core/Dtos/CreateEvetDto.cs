using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class CreateEvetDto
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;
        public  int  InitiatorId { get; set; }

        public int BirthdayBoyId { get; set; }
    }
}
