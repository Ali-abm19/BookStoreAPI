using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.src.Entity;
using BookStore.src.Repository;
using BookStore.src.Utils;
using static BookStore.src.DTO.UserDTO;

namespace BookStore.src.Services.user
{
    public class UserService : IUserService
    {
        protected readonly UserRepository _userRepo;
        protected readonly IMapper _mapper;
        protected readonly IConfiguration _config;

        public UserService(UserRepository userRepo, IMapper mapper, IConfiguration config)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _config = config;
        }
        /*        public async Task<UserReadDto> CreateOneAsync(UserCreateDto createDto)
                {
                    var user = _mapper.Map<UserCreateDto, User>(createDto);
                    var userCreated = await _userRepo.CreateOneAsync(user);
                    return _mapper.Map<User, UserReadDto>(userCreated);
                }*/

        public async Task<List<UserReadDto>> GetAllAsync()
        {
            var userList = await _userRepo.GetAllAsync();
            return _mapper.Map<List<User>, List<UserReadDto>>(userList);
        }


        public async Task<UserReadDto> GetByIdAsync(Guid id)
        {
            var foundUser = await _userRepo.GetByIdAsync(id);
            return _mapper.Map<User, UserReadDto>(foundUser);
        }


        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundUser = await _userRepo.GetByIdAsync(id);
            bool isDeleted = await _userRepo.DeleteOneAsync(foundUser);

            if (isDeleted)
            {
                return true;
            }
            return false;
        }


        public async Task<bool> UpdateOneAsync(Guid id, UserUpdateDto updateDto)
        {
            var foundUser = await _userRepo.GetByIdAsync(id);

            if (foundUser == null)
            {
                return false;
            }

            _mapper.Map(updateDto, foundUser);
            return await _userRepo.UpdateOneAsync(foundUser);
        }

        //  public Task<string> SignInAsync(UserCreateDto createDto)
        //  {throw new NotImplementedException();}

        public async Task<UserReadDto> CreateOneAsync(UserCreateDto createDto)
        {

            PasswordUtils.Password(createDto.Password, out string hashedPassword, out byte[] salt);

            var user = _mapper.Map<UserCreateDto, User>(createDto);
            user.Password = hashedPassword;
            user.Salt = salt;
            user.Role = Role.Customer;


            var savedUser = await _userRepo.CreateOneAsync(user);
            return _mapper.Map<User, UserReadDto>(savedUser);

        }

//public static bool VerifyPassword(string plainPassword, byte[] salt, string hashedPassword)

        public async Task<string> SignInAsync(UserCreateDto createDto)
        {
            var foundUser = await _userRepo.FindByEmailAsync(createDto.Email);
            var isMatched = PasswordUtils.
            VerifyPassword(createDto.Password, foundUser.Salt, foundUser.Password);

            if (isMatched)
            {
                var tokenUtil = new TokenUtils(_config);
                return tokenUtil.GnerateToken(foundUser);
            }
            return "Unauthorized";
        }

    }
}