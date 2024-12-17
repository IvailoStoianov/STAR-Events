using Microsoft.AspNetCore.Identity;
using STAREvents.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Services.Data.Interfaces
{
    public interface IUserAuthService
    {
        Task<SignInResult> LoginAsync(string username, string password);
        Task<IdentityResult> RegisterAsync(string username, string password, string email);
        Task LogoutAsync();
        Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword);
        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<IdentityResult> AddRoleToUserAsync(string userId, string roleName);
        Task<IdentityResult> RemoveRoleFromUserAsync(string userId, string roleName);
        Task<IList<string>> GetUserRolesAsync(string userId);
        Task<ApplicationUser?> GetUserByIdAsync(string userId);
        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser?> GetUserByNameAsync(string username);
        Task<bool> IsUserInRoleAsync(string userId, string roleName);
    }

}
