using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class CreateVoteDto
    {
        public int EventId { get; set; }
        public int GiftId { get; set; }
        public int VoterId { get; set; }
    }
}
