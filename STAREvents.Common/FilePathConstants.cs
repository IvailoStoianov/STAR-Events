using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Common
{
    public static class FilePathConstants
    {
        public static class SeedDataPaths
        {
            public const string CategoriesSeedPath = @"SeedData\categories.json";
            public const string EventsSeedPath = @"SeedData\events.json";
        }
        public static class ProfilePicturePaths
        {
            public const string DefaultProfilePicturePath = "images/profile-pictures";
        }
        public static class EventPicturePaths
        {
            public const string DefaultEventPicturePath = "images/event-images";
        }
        public static class AzureContainerNames
        {
            public const string ProfilePicturesContainer = "profile-pictures";
            public const string EventImagesContainer = "event-images";
        }
    }
}
