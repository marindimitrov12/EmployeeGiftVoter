﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class EventResponseDto
    {
        public  int EventId { get; set; }
        public string StartDate { get; set; }
        public int InitiatorId { get; set; }
        public int BirthdayBoyId { get; set; }
        public  string BirthdayBoyName { get; set; }
        public string EndDate { get; set; }
        public List<EventResultDto> Results { get; set; }

        public string ImgUrl { get; set; }
    }
}
