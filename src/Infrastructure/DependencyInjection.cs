using AspNet.Security.OpenIdConnect.Primitives;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain;
using FoodOnline.Infrastructure.Identity;
using FoodOnline.Infrastructure.Persistence;
using FoodOnline.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Validation;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;

namespace FoodOnline.Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Add ( <see cref="ApplicationDbContext"/> ) <br/>
        /// Add ( <see cref="FoodOnlineDbContext"/> ) with UseOpenIddict(). <br/>
        /// Add Scoped <see cref="IApplicationDbContext"/>
        /// Add OpenIddict <br/>
        /// Add JwtBearer <br/>
        /// Add Transient ( <see cref="IDateTime"/>, <see cref="DateTimeService"/> ) <br/>
        /// Add Transient ( <see cref="IIdentityService"/>, <see cref="FoodOnlineUserManager"/> )
        /// Add Transient ( <see cref="FoodOnlineUserManager"/> )
        /// </summary>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddDbContext<ApplicationDbContext>(conf =>
                conf.UseMySql(configuration.GetConnectionString("FoodOnlineAppDb")));

            services.AddDbContext<FoodOnlineDbContext>(conf =>
            {
                conf.UseMySql(configuration.GetConnectionString("FoodOnlineIdentityDb"));
                conf.UseOpenIddict();
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services
                .AddOpenIddict()
                .AddCore(conf => conf.UseEntityFrameworkCore().UseDbContext<FoodOnlineDbContext>())
                .AddServer(conf =>
                {
                    conf.UseMvc();
                    conf.EnableTokenEndpoint("/api/auth/token")
                        .EnableLogoutEndpoint("/api/auth/logout");

                    conf.RegisterScopes(
                        OpenIddictConstants.Scopes.Roles,
                        OpenIdConnectConstants.Scopes.OfflineAccess);

                    conf.UseJsonWebTokens()
                        .SetAccessTokenLifetime(TimeSpan.FromDays(7));

                    conf.AddSigningKey(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsAgReatSingingKey")));

                    conf.AllowPasswordFlow()
                        .AllowRefreshTokenFlow();

                    conf.EnableRequestCaching();

                    conf.AcceptAnonymousClients()
                        .DisableHttpsRequirement();
                });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
            services.AddAuthentication(OpenIddictValidationDefaults.AuthenticationScheme)
                .AddJwtBearer(conf =>
                {
                    conf.RequireHttpsMetadata = false;
                    conf.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = OpenIdConnectConstants.Claims.Name,
                        RoleClaimType = OpenIdConnectConstants.Claims.Role,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsAgReatSingingKey")),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                    conf.SaveToken = true;
                });

            services.AddAuthorization(conf =>
            {
                conf.AddPolicy(Policy.Staff, policy =>
                    policy.RequireRole(Role.Admin, Role.Employee));

                conf.AddPolicy(Policy.StaffAndOwner, policy =>
                    policy.RequireRole(Role.Admin, Role.Employee, Role.StoreOwner));

                conf.AddPolicy(Policy.StaffAndCustomer, policy =>
                    policy.RequireRole(Role.Admin, Role.Employee, Role.StoreOwner, Role.StoreEmployee));

                conf.AddPolicy(Policy.Customer, policy =>
                    policy.RequireRole(Role.StoreOwner, Role.StoreEmployee));
            });

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, FoodOnlineUserManager>();
            services.AddTransient<FoodOnlineUserManager>();

            return services;
        }
    }
}