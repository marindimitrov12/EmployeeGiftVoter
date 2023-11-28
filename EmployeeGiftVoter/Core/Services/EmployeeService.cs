using Core.Dtos;
using Core.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class EmployeeService : IEmployeesService
    {
        private readonly ApplicationDbContext _context;
        public EmployeeService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<List<ClientResponseDto>> GetAll()
        {
            var emp = await _context.Employees.ToListAsync();
            var result=new List<ClientResponseDto>();
            foreach (var item in emp)
            {
                result.Add(new ClientResponseDto
                {
                    Id=item.Id,
                    EmployeeName=item.EmployeeName,
                    DateOfBirt=item.DateOfBirt.ToString(),
                    Username=item.Username,
                    ImgUrl=item.ImgUrl,
                    
                });
            }
            return result;
        }

        public async Task<List<GiftDto>> GetAllGifts()
        {
            var gifts=await _context.Gifts.ToListAsync();
            var result=new List<GiftDto>();
            foreach (var item in gifts)
            {
                result.Add(new GiftDto
                {
                    Id=item.Id,
                    Name=item.GiftName,
                    ImgUrl = item.ImgUrl
                });
            }
            return result;
        }

        public async Task<List<EventResponseDto>> GetMyEvents(int id)
        {
            var myEvents = await _context.Events
                .Include(x=>x.Initiator)
                .Include(x=>x.BirthdayBoy)
                .Where(x => x.InitiatorId == id)
                .ToListAsync();
            var result = new List<EventResponseDto>();
            foreach (var item in myEvents)
            {
                result.Add(new EventResponseDto
                {
                    StartDate = item.StartDate.ToString(),
                    BirthdayBoyId = item.BirthdayBoyId,
                    BirthdayBoyName = item.BirthdayBoy.EmployeeName,
                    EndDate = item.EndDate.ToString(),
                    EventId=item.Id,
                    InitiatorId=item.Initiator.Id,
                    
                }) ;
            }
            return result;
        }
    }
}
