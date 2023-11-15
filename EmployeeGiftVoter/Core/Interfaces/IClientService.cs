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
        Task <EventResponseDto>StartEvent(CreateEvetDto dto);
        Task<List<EventResponseDto>> GetAllEvents(int bdayBoyId);
        Task<ClientResponseDto>Login(LogInClientDto user);
        Task<ClientResponseDto> Register(RegisterClientDto user);
        Task<VoteResponseDto> Vote(CreateVoteDto vote);
        Task<CloseEventDto>CloseEvent(CloseEventDto dto);
        
    }
}
