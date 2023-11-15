using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IClientService
    {
        Task CreateEvent(CreateEvetDto dto);
        Task<ClientResponseDto>Login(LogInClientDto user);
        Task<ClientResponseDto> Register(RegisterClientDto user);   
    }
}
