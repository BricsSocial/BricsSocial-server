using BricsSocial.Application.Common.Models;

namespace BricsSocial.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<UserInfo?> GetUserInfoAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<(Result Result, string UserId)> CreateUserAsync(UserInfo userInfo, string password);

    Task<Result> DeleteUserAsync(string userId);
}