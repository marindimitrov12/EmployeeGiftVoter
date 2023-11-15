using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class ClientResponseDto
    {
        public string EmployeeName { get; set; }
        public string Username { get; set; }
        public string DateOfBirt { get; set; }
        public  string  AccessToken { get; set; }
    }
}
