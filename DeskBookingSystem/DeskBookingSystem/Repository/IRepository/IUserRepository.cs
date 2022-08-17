using DeskBookingSystem.Models;
using DeskBookingSystem.Models.DTOs;

namespace DeskBookingSystem.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUnique(string username);

        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO);
    }
}
