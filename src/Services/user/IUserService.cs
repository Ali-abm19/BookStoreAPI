using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookStore.src.DTO.UserDTO;

namespace BookStore.src.Services.user
{
    public interface IUserService
    {
        Task<UserReadDto> CreateOneAsync(UserCreateDto createDto);
        Task<List<UserReadDto>> GetAllAsync();
        Task<UserReadDto> GetByIdAsync(Guid id);
        Task<bool> DeleteOneAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, UserUpdateDto updateDto);
        Task<UserSignedInInfoDto> SignInAsync(UserSigninDto createDto);
    }
}