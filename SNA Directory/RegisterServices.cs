namespace SNA_Directory;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddMemoryCache(); 

        builder.Services.AddSingleton<IDbConnection,DbConnection>();
        builder.Services.AddSingleton<IAreaData,AreaData>();
        builder.Services.AddSingleton<IBiomeData, BiomeData>();
        builder.Services.AddSingleton<ICommentData, CommentData>();
    }
}
