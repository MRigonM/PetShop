using PetShop.Extensions;
using PetShop.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddModule(new CoreModule(builder.Configuration));
builder.Services.AddModule(new AuthModule());
builder.Services.AddModule(new DataModule(builder.Configuration));

var app = builder.Build();

await app.UseDataSeeder();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();