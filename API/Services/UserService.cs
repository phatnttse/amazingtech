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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;


        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, ITokenService tokenService,
            RoleManager<IdentityRole> roleManager
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            var userRepository = _unitOfWork.GetRepository<User>();
            var users = await userRepository.GetQueryable().Where(u => u.Active).ToListAsync();
            return users;
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            var userRepository = _unitOfWork.GetRepository<User>();
            var user = await userRepository.GetByIdAsync(id);

            if (user == null) throw new Exception("User does not exist");
            if (!user.Active) throw new Exception("User is deleted");

            return user;
        }

        public async Task<User> UpdateUserAsync(string id, UpdateUserDto updateUserDto)
        {
            var userRepository = _unitOfWork.GetRepository<User>();

            var existingUser = await userRepository.GetByIdAsync(id);

            if (existingUser == null) throw new Exception("User does not exist");
            if (!existingUser.Active) throw new Exception("User is deleted");


            _mapper.Map(updateUserDto, existingUser);

            await userRepository.UpdateAsync(existingUser);

            await _unitOfWork.SaveChangesAsync();

            return existingUser;
        }

        public async Task DeleteUserAsync(string id)
        {
            var userRepository = _unitOfWork.GetRepository<User>();

            var existingUser = await userRepository.GetByIdAsync(id);

            if (existingUser == null) throw new Exception("User does not exist");
            if (!existingUser.Active) throw new Exception("User is deleted");

            existingUser.Active = false;
            await userRepository.UpdateAsync(existingUser);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<User?> Register(RegisterDto registerDto)
        {

            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
                throw new Exception($"Email {registerDto.Email} already taken");

            var newUser = _mapper.Map<User>(registerDto);

            var result = await _userManager.CreateAsync(newUser, registerDto.Password);


            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });

                await _userManager.AddToRoleAsync(newUser, "Admin");
            }
            else
            {
                if (!await _roleManager.RoleExistsAsync("Employee"))
                    await _roleManager.CreateAsync(new IdentityRole { Name = "Employee" });


                await _userManager.AddToRoleAsync(newUser, "Employee");

            }

            if (result.Succeeded)
            {

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
            if (!user.Active) throw new Exception("User is deleted");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (result)
            {
                var token = _tokenService.CreateToken(user);

                return new LoginResponse { Message = "Login Successfully", Token = token.Result };
            }

            return new LoginResponse { Message = "Password incorrect", Token = null };

        }

    }

}
