using API.Dtos;
using API.Models;
using API.Repositories;
using API.Responses;
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
        //private readonly RoleManager<Role> _roleManager;
        private readonly ITokenService _tokenService;


        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, ITokenService tokenService
            //RoleManager<Role> roleManager
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
            //_roleManager = roleManager;
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
                throw new Exception("User does not exist");


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

        public async Task<User?> Register(RegisterDto registerDto)
        {

            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
                throw new Exception($"Email {registerDto.Email} already taken");

            //if (!await _roleManager.RoleExistsAsync("Manager"))
            //{
            //    await _roleManager.CreateAsync(new Role { Name = "Manager" });
            //}

            var newUser = _mapper.Map<User>(registerDto);

            var result = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "Manager");

                await _unitOfWork.SaveChangesAsync();

                return newUser;
            }

            else
            {
                result.Errors.ToList().ForEach(e =>
                {
                    throw new Exception(e.Description);
                });

                return null;
            }

        }

        public async Task<LoginResponse> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) throw new Exception("User does not exist");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (result)
            {
                var token = _tokenService.CreateToken(user);

                return new LoginResponse { Message = "Login Successfully", Token = token };
            }

            return new LoginResponse { Message = "Password incorrect", Token = null };

        }

    }

}
