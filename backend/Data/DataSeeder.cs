using System.Threading.Tasks;

namespace Data
{
    public class DataSeeder
    {
        public static async Task Seed(DatabaseContext databaseContext)
        {
            // if (!userManager.Users.Any())
            // {
            //     var users = new List<AppUser>
            //     {
            //         new AppUser
            //         {
            //             Id = Path.GetRandomFileName().Replace(".", ""),
            //             FullName = "James Potter",
            //             UserName = "prongs",
            //             Email = "james@potter.com"
            //         },
            //         new AppUser
            //         {
            //             Id = Path.GetRandomFileName().Replace(".", ""),
            //             FullName = "Sirius Black",
            //             UserName = "padfoot",
            //             Email = "sirius@black.com"
            //         },
            //         new AppUser
            //         {
            //             Id = Path.GetRandomFileName().Replace(".", ""),
            //             FullName = "Remus Lupin",
            //             UserName = "moony",
            //             Email = "remus@lupin.com"
            //         },
            //         new AppUser
            //         {
            //             Id = Path.GetRandomFileName().Replace(".", ""),
            //             FullName = "Peter Pettigrew",
            //             UserName = "wormtail",
            //             Email = "peter@pettigrew.com"
            //         },
            //     };

            //     foreach (var user in users)
            //         await userManager.CreateAsync(user, "Pa$$w0rd");
            // }

            await databaseContext.SaveChangesAsync();
        }
    }
}