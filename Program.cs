using Microsoft.EntityFrameworkCore;
using yogago.Models;

namespace yogago
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ModelContext>(options =>
 options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"))
        .LogTo(Console.WriteLine, LogLevel.Information));
            //builder.Services.AddDbContext<ModelContext>(x => x.UseOracle(builder.Configuration.GetConnectionString("YoGAConnection")));
            //builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30);
			});
			var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
			builder.Services.AddDistributedMemoryCache();

			app.UseHttpsRedirection();
			app.UseStaticFiles();


			app.UseRouting();
			app.UseSession();
			app.UseAuthorization();

			//app.MapDefaultControllerRoute();



            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}