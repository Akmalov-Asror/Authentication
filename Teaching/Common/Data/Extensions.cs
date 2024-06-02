using Microsoft.EntityFrameworkCore;

namespace Teaching.Common.Data;

public static class Extensions
{
    public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();
        var configuration = provider.GetRequiredService<IConfiguration>();
        var sqlConnectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(sqlConnectionString);
        });

        return services;
    }
}
