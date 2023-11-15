using Core.Dtos;
using Core.Interfaces;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task CreateEvent(CreateEvetDto dto)
        {

             await _context.Events.AddAsync(new Event { StartDate = DateTime.Now, InitiatorId = dto.InitiatorId,BirthdayBoyId =dto.BirthdayBoyId});
            await _context.SaveChangesAsync();
            
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
    }
}
