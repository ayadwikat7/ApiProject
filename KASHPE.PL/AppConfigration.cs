using BLL.Services;
using DAL.Repository;
using DAL.Utils;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace KASHPE.PL
{//session 12 part 2
    public static class AppConfigration
    {
        public static void config(IServiceCollection Services) {

           Services.AddScoped<ICategoryRepository, CategoryRepository>();
            Services.AddScoped<ICategorySevices, CategoryService>();

            Services.AddScoped<IAuthanication, Authanication>();
           Services.AddTransient<IEmailSender, EmailSender>();
            Services.AddScoped<ISeedData, RoleSeedData>();
           Services.AddScoped<ISeedData, UserSeedDate>();
        }
    }
}
