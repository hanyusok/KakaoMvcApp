using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KakaoMvcApp.Data;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddKakaoTalk(o => 
{
    o.ClientId = "f92ca1e808e4f3a5cfa5ee858601bb8e";
    o.ClientSecret = "FnwsbM4lNPyi7NdzLs3tB4LS5nOPteU4";    
    o.SaveTokens = true;
    
    o.Events.OnCreatingTicket = ctx =>
    {
        List<AuthenticationToken> tokens = ctx.Properties.GetTokens().ToList(); 

        tokens.Add(new AuthenticationToken()
        {
            Name = "TicketCreated", 
            Value = DateTime.UtcNow.ToString()
        });

        ctx.Properties.StoreTokens(tokens);

        return Task.CompletedTask;
    };
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(o => o.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
// builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
