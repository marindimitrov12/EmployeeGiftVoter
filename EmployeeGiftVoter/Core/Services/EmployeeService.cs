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
                    
                });
            }
            return result;
        }
    }
}
