﻿using DanceConnect.Server.Entities;

namespace DanceConnect.Server.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<User> ApproveUserAsync(int id);
        Task<User> DeclineUserAsync(int id);
    }
}
