using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Common
{
    public static class SuccessMessage
    {
        public static class Profile
        {
            public const string ProfileUpdated = "Profile updated successfully.";
            public const string PasswordChanged = "Password changed successfully.";
            public const string ProfileDeleted = "Profile deleted successfully.";
        }
        public static class Admin
        {
            public const string EventDeleted = "Event deleted successfully.";
            public const string EventRecovered = "Event recovered successfully.";
            public const string UserDeleted = "User deleted successfully.";
            public const string UserRecovered = "User recovered successfully.";
            public const string AdminRoleAdded = "Admin role added successfully.";
            public const string AdminRoleRemoved = "Admin role removed successfully.";
        }
    }
}
