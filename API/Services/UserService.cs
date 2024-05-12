using API.Dtos;
using API.Models;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;


        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            var userRepository = _unitOfWork.GetRepository<User>();
            var users = await userRepository.GetAllAsync();
            return users;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            var userRepository = _unitOfWork.GetRepository<User>();
            var user = await userRepository.GetByIdAsync(id);
            return user;
        }

        public async Task<User> UpdateUserAsync(string id, UserDto userDto)
        {
            var userRepository = _unitOfWork.GetRepository<User>();
            var existingUser = await userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new Exception("User does not exist");
            }
            _mapper.Map(userDto, existingUser);
            await userRepository.UpdateAsync(existingUser);
            await _unitOfWork.SaveChangesAsync();
            return existingUser;
        }

        public async Task DeleteUserAsync(string id)
        {
            var userRepository = _unitOfWork.GetRepository<User>();
            var deletedUser = await userRepository.GetByIdAsync(id);
            deletedUser.Active = false;
            await userRepository.UpdateAsync(deletedUser);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<User> Register(RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
                throw new Exception($"Email {registerDto.Email} already taken");


            var newUser = _mapper.Map<User>(registerDto);

            var result = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (result.Succeeded)
            {
                await _unitOfWork.SaveChangesAsync();
                return newUser;
            }

            else
            {
                var errors = result.Errors;

                foreach (var error in errors)
                {
                    throw new Exception(error.Description);
                }
                return null;
            }

        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) throw new Exception("User does not exist");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (result)
            {
                var token = _tokenService.CreateToken(user);

                return token;
            }

            return null;

        }

    }

}
