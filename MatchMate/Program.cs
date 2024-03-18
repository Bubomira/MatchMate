
using MatchMate.ModelBinders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AttachDbContext(builder.Configuration)
    .AttachIdentity()
    .AttachServices();

builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews().AddMvcOptions(options=>
      options.ModelBinderProviders.Insert(0,new DateModelBinderProvider()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
