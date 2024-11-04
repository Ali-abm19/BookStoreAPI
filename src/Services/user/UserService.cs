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

        public async Task<List<UserReadDto>> GetAllAsync()
        {
            var userList = await _userRepo.GetAllAsync();
            return _mapper.Map<List<User>, List<UserReadDto>>(userList);
        }

        public async Task<UserReadDto> GetByIdAsync(Guid id)
        {
            var foundUser = await _userRepo.GetByIdAsync(id);
            if (foundUser == null)
            {
                throw CustomException.NotFound($"user with {id} cannot be found! ");
            }
            return _mapper.Map<User, UserReadDto>(foundUser);
        }

        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundUser = await _userRepo.GetByIdAsync(id);
            if (foundUser == null)
            {
                throw CustomException.NotFound($"user with ID {id} cannot be found for deletion.");
            }
            try
            {
                bool isDeleted = await _userRepo.DeleteOneAsync(foundUser);
                return isDeleted;
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError(
                    $"An error occurred while deleting the user with ID {id}: {ex.Message}"
                );
            }
        }

        public async Task<UserReadDto> UpdateOneAsync(Guid id, UserUpdateDto updateDto)
        {
            if (!string.IsNullOrWhiteSpace(updateDto.Password))
            {
                PasswordUtils.Password(
                    updateDto.Password,
                    out string hashedPassword,
                    out byte[] salt
                );
                updateDto.Password = hashedPassword;
                var foundUser = await _userRepo.GetByIdAsync(id);

                if (foundUser == null)
                {
                    throw CustomException.NotFound(
                        $"user with ID {id} cannot be found for updating."
                    );
                }

                _mapper.Map(updateDto, foundUser);
                foundUser.Salt = salt;

                //return await _userRepo.UpdateOneAsync(foundUser);
                return _mapper.Map<User, UserReadDto>(await _userRepo.UpdateOneAsync(foundUser));
            }
            else
            {
                var foundUser = await _userRepo.GetByIdAsync(id);
                if (foundUser == null)
                {
                    throw CustomException.NotFound(
                        $"user with ID {id} cannot be found for updating."
                    );
                }
                updateDto.Password = foundUser.Password;
                _mapper.Map(updateDto, foundUser);
                return _mapper.Map<User, UserReadDto>(await _userRepo.UpdateOneAsync(foundUser));
            }
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

        public async Task<UserSignedInInfoDto> SignInAsync(UserSigninDto createDto)
        {
            bool isMatched = false;
            //var foundUser = await _userRepo.FindByEmailAsync(createDto.Email);
            List<User> userList = await _userRepo.GetAllAsync();
            var foundUser = userList.FirstOrDefault(u => u.Email == createDto.Email);
            if (foundUser != null)
            {
                isMatched = PasswordUtils.VerifyPassword(
                    createDto.Password,
                    foundUser.Salt,
                    foundUser.Password
                );

                if (isMatched)
                {
                    var tokenUtil = new TokenUtils(_config);
                    UserSignedInInfoDto u =
                        new()
                        {
                            dto = _mapper.Map<User, UserReadDto>(foundUser),
                            Token = tokenUtil.GnerateToken(foundUser),
                        };

                    return u;
                }
                throw CustomException.UnAuthorized($"The Password or Email are wrong");
            }
            else
            {
                //return "Not Found";
                //When users entered email address does not exist in our database
                throw CustomException.NotFound($"User with email {createDto.Email} was not found");
            }
        }
    }
}
