using ATON_ITTP_2024_Domain;
using Microsoft.EntityFrameworkCore;
using ATON_ITTP_2024.Service;

namespace ATON_ITTP_2024.Utilities
{
    public static class MigrationHelper
    {
        public static bool DatabaseExist(DbContextOptions options)
        {
            using ITTP_2024_Context userContext = new ITTP_2024_Context(options);
            return userContext.Database.CanConnect();
        }

        public static bool MigrationsExist(DbContextOptions options)
        {
            using ITTP_2024_Context userContext = new ITTP_2024_Context(options);
            return userContext.Database.GetMigrations().Count() > 0;
        }

        public static void CreateDatabase(DbContextOptions options, IWebHostEnvironment webHostEnvironment)
        {
            using ITTP_2024_Context userContext = new ITTP_2024_Context(options);
            if (userContext.Database.EnsureCreated())
            {
                UserService userService = new UserService(userContext, webHostEnvironment);
                userService.createAdmin().Wait();
            }
        }
    }
}
