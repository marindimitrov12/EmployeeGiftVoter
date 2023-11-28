using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeName { get;set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        public DateTime DateOfBirt { get; set; }
        public List<Event> Events { get; set; }
        public string ImgUrl { get; set; }
    }
}
