using Core.Dtos;
using Core.Interfaces;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;
        public ClientService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<EventResponseDto> StartEvent(CreateEvetDto dto)
        {
            var ev = await _context.Events.FirstOrDefaultAsync(x=>x.BirthdayBoyId==dto.BirthdayBoyId);

            if (ev==null)
            {
                await _context.Events.AddAsync(new Event { StartDate = dto.StartDate, InitiatorId = dto.InitiatorId, BirthdayBoyId = dto.BirthdayBoyId, EndDate = null });
                await _context.SaveChangesAsync();
                return new EventResponseDto()
                {
                    StartDate = dto.StartDate.ToString(),
                    EndDate = dto.EndDate,
                    BirthdayBoyId = dto.BirthdayBoyId,
                    InitiatorId = dto.InitiatorId,


                };
            }
            else
            {
                throw new Exception("The event for this user is already started!!!");
            }
            
            
        }

        public async Task<ClientResponseDto> Login(LogInClientDto user)
        {
            var client = await _context.Employees.FirstOrDefaultAsync(x=>x.Username==user.UserName);
            if (client == null)
            {
                throw new ArgumentException("Company not found");
            }
            if (!VerifyPassword(user.Password,client.PasswordHash,client.PasswordSalt))
            {
                throw new ArgumentException("Wrong password");
            }
            return new ClientResponseDto
            {
                DateOfBirt = client.DateOfBirt.ToString(),
                EmployeeName = client.EmployeeName,
                Username = user.UserName,
            };
        }

        public async Task<ClientResponseDto> Register(RegisterClientDto user)
        {
            Employee empEntity = new Employee()
            {
                EmployeeName= user.EmployeeName,
                DateOfBirt=DateTime.Parse(user.DateOfBirth),
                Username=user.Username,
                
            };
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            empEntity.PasswordSalt = passwordSalt;
            empEntity.PasswordHash= passwordHash;
            await _context.AddAsync<Employee>(empEntity);
            await _context.SaveChangesAsync();
            return new ClientResponseDto
            {
                Username = user.Username,
                EmployeeName = user.EmployeeName,
                DateOfBirt = user.DateOfBirth
            };
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public async Task<List<EventResponseDto>> GetAllEvents(int bdayBoyId)
        {
            var events = await _context.Events.Where(x=>x.BirthdayBoyId!=bdayBoyId).ToListAsync();
            var result = new List<EventResponseDto>();
            foreach (var item in events)
            {
                result.Add(new EventResponseDto
                {
                    StartDate=item.StartDate.ToString(),
                    BirthdayBoyId=item.BirthdayBoyId,
                    EndDate=item.EndDate.ToString(),
                    InitiatorId=item.InitiatorId,
                    


                });
            }
            return result;
        }

        public async Task<VoteResponseDto> Vote(CreateVoteDto vote)
        {
            var result = await _context.EventResults.FirstOrDefaultAsync(x=>x.VoterId==vote.VoterId);
            var ev = await _context.Events.FirstOrDefaultAsync(x=>x.Id==vote.EventId);
            var birthDayBoy = await _context.Events.FirstOrDefaultAsync(x => x.BirthdayBoyId == ev.BirthdayBoyId);
            if (birthDayBoy.BirthdayBoyId==vote.VoterId) 
            {
                throw new Exception("The BirthDay boy cant vote!!!");
            }
            if (result==null)
            {
                await _context.EventResults.AddAsync(new EventResult
                { GiftId = vote.GiftId, EventId = vote.EventId, VoterId = vote.VoterId });
                await _context.SaveChangesAsync();
                return new VoteResponseDto
                {
                    EventId = vote.EventId,
                    VoterId = vote.VoterId,
                    GiftId = vote.GiftId
                };
            }
            else
            {
                throw new Exception("You can vote just Once!");
            }
           
           
        }

        public async Task<CloseEventDto> CloseEvent(CloseEventDto dto)
        {
            
            var editedEntity = await _context.Events.FirstOrDefaultAsync(x=>x.Id==dto.EventId&&x.InitiatorId==dto.EmployeeId);
            if (editedEntity!=null)
            {
                editedEntity.EndDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return new CloseEventDto
                {
                    EndDate = editedEntity.EndDate.ToString(),
                    EventId = dto.EventId,
                };
            }
            else
            {
                throw new Exception("Specified event doent exist!!!");
            }
            
        }
    }
}
