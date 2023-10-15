using FoodOnline.UI.Web.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

//app.MapRazorComponents<App>();
//app.MapGroup("test").MapRazorComponents<App>();

//app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/test"), test =>
//{
//    test.UseRouting();

//    var auth = test.ApplicationServices.GetRequiredService<IAuthorizationService>();


//    auth.AuthorizeAsync()

//    test.UseEndpoints(endpoints =>
//    {
//        endpoints.MapRazorComponents<App>();


//        endpoints.MapControllerRoute("", "v1");
//    });
//});

app.Run();
