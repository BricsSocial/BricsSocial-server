using BricsSocial.Application.Common.Models;

namespace BricsSocial.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<UserInfo?> GetUserInfoAsync(string userId);
    Task<UserInfo?> GetUserInfoByEmailAsync(string email);

    Task<bool> CheckPasswordAsync(string email, string password);

    Task<(Result Result, string UserId)> CreateUserAsync(UserInfo userInfo, string password);
    Task<Result> DeleteUserAsync(string userId);
}