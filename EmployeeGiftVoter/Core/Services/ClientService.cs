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

            if (ev==null||ev.StartDate.Year+1==dto.StartDate.Year)
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
                Id = client.Id,
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
            var events = await _context.Events.Include(x=>x.BirthdayBoy).Where(x=>x.BirthdayBoyId!=bdayBoyId).ToListAsync();
            var result = new List<EventResponseDto>();
            foreach (var item in events)
            {
                result.Add(new EventResponseDto
                {
                    EventId=item.Id,
                    StartDate=item.StartDate.ToString(),
                    BirthdayBoyId=item.BirthdayBoyId,
                    EndDate=item.EndDate.ToString(),
                    InitiatorId=item.InitiatorId,
                    BirthdayBoyName=item.BirthdayBoy.EmployeeName


                });
            }
            return result;
        }

        public async Task<VoteResponseDto> Vote(CreateVoteDto vote)
        {
            var result = await _context.EventResults.Where(x=>x.EventId==vote.EventId)
                .Select(x => x.VoterId)
                .ToListAsync();
            var ev = await _context.Events.FirstOrDefaultAsync(x=>x.Id==vote.EventId);
            var birthDayBoy = await _context.Events.FirstOrDefaultAsync(x => x.BirthdayBoyId == ev.BirthdayBoyId);
            if (birthDayBoy.BirthdayBoyId==vote.VoterId) 
            {
                throw new Exception("The BirthDay boy cant vote!!!");
            }
            if (!result.Contains(vote.VoterId))
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
            if (editedEntity!=null&&editedEntity.EndDate==null)
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
                throw new Exception("Specified event doent exist or event is alredy closed!!!");
            }
            
        }

        public async Task<List<ResultDto>> GetResults(ResultRequestDto result)
        {
            var gifts=await _context.Gifts.ToListAsync();
            var ev = await _context.Events.Include(x=>x.Results).FirstOrDefaultAsync(x=>x.Id==result.EventId);
            if (ev.EndDate==null) 
            {
                throw new Exception("Event still open!");
            }
            if (ev.BirthdayBoyId==result.PersonId) 
            {
                throw new Exception("You can't see this results!");
            }
            var resultDic=new Dictionary<string, int>();
            foreach (var gift in gifts) 
            {
                resultDic.Add(gift.GiftName,0);
            }
            foreach (var item in ev.Results)
            {
                resultDic[item.Gift.GiftName] += 1;
            }
            resultDic.OrderByDescending(x=>x.Value);
            var endResult = new List<ResultDto>();
            foreach (var item in resultDic)
            {
                endResult.Add(new ResultDto 
                { 
                    GiftName=item.Key,
                    Count=item.Value,
                   
                    
                });
            }
            return endResult;
        }

        public async  Task<List<TrackVotingDto>> TrackVoting(ResultRequestDto result)
        {
            var ev = await _context.Events.Include(x => x.Results).ThenInclude(x=>x.Gift).FirstOrDefaultAsync(x => x.Id == result.EventId);
            var allEmp = await _context.Employees.ToListAsync();
            var res = new List<TrackVotingDto>();
            if (ev.BirthdayBoyId==result.PersonId)
            {
                throw new Exception("You can't see this results!");
            }
            foreach (var item in allEmp)
            {
                if (ev.Results.FirstOrDefault(x=>x.VoterId==item.Id)!=null)
                {
                    res.Add(new TrackVotingDto
                    {
                        VoterName = ev.Results.FirstOrDefault(x => x.VoterId == item.Id).Voter.EmployeeName,
                        GiftVoted = ev.Results.FirstOrDefault(x => x.VoterId == item.Id).Gift.GiftName,
                    });
                }
                else
                {
                    res.Add(new TrackVotingDto
                    {
                        VoterName = item.EmployeeName,
                        GiftVoted = null
                    });
                }
            }
            return res;

        }
    }
}
