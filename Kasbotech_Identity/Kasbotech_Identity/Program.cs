using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;
using Kasbotech_Identity.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<KasbonetIdentityDbContext>()
    .AddDefaultTokenProviders();
IConfiguration configuration = builder.Configuration;
builder.Services.AddDbContext<KasbonetIdentityDbContext>(p => p.UseSqlServer(configuration["AspIdentityConnection"]));

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryClients(new List<Client>
    {
        new Client
        {
            ClientName = "ClientUser",
            ClientId = "user",
            ClientSecrets = {new Secret("123456".Sha256())},
            AllowedGrantTypes = GrantTypes.Code,
            RedirectUris ={ "http://localhost:7018/signin-oidc"},
            PostLogoutRedirectUris = {"https://localhost:7018/signout-callback-oidc"},
            AllowedScopes = { "profile","openid","CandleService.RW" },
           AllowOfflineAccess = true,
           AccessTokenLifetime = 60,
           RefreshTokenUsage = TokenUsage.ReUse,
           RefreshTokenExpiration = TokenExpiration.Sliding,

        }
    })
    .AddInMemoryIdentityResources(new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    })
    .AddInMemoryApiScopes(new List<ApiScope>
    {
        new ApiScope("CandleService.RW")
    })
    .AddInMemoryApiResources(new List<ApiResource>
    {
        new ApiResource("CandleService","Candle Service Api")
        {
            Scopes = { "CandleService.RW" }
        }

    })
    .AddAspNetIdentity<IdentityUser>();



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

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
