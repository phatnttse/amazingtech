using API.Dtos;
using API.Models;
using API.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        public async Task<User> CreateUserAsync(UserDto userDto)
        {
            var userRepository = _unitOfWork.GetRepository<User>();
            var user = _mapper.Map<User>(userDto);
            await userRepository.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();
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
    }

}
