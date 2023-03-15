using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BricsSocial.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;;
    }

    public async Task<UserInfo?> GetUserInfoAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return null;

        return await ToUserInfo(user);
    }

    public async Task<UserInfo?> GetUserInfoByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return null;

        return await ToUserInfo(user);
    }

    private async Task<UserInfo> ToUserInfo(ApplicationUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);
        var userInfo = new UserInfo
        {
            UserId = user.Id,
            Email = user.Email,
            Role = userRoles.FirstOrDefault(),
            FirstName = user.FirstName,
            LastName = user.LastName,
        };
        return userInfo;
    }

    public async Task<bool> CheckPasswordAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return false;

        var passwordCheck = await _userManager.CheckPasswordAsync(user, password);

        return passwordCheck;
    }

    public async Task<(Result Result, string? UserId)> CreateUserAsync(UserInfo userInfo, string password)
    {
        var user = new ApplicationUser
        {
            UserName = userInfo.Email,
            Email = userInfo.Email,
            FirstName = userInfo.FirstName,
            LastName = userInfo.LastName
        };

        var createUserResult = await _userManager.CreateAsync(user, password);
        if (!createUserResult.Succeeded)
            return (createUserResult.ToApplicationResult(), null);

        var addToRoleResult = await _userManager.AddToRoleAsync(user, userInfo.Role);
        if (!addToRoleResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);
            return (addToRoleResult.ToApplicationResult(), null);
        }

        return (createUserResult.ToApplicationResult(), user.Id);
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }


}
