﻿using FoodOnline.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOnline.Authentications;

public sealed class AuthenticationsModule : IInfrastructureModule
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        throw new NotImplementedException();
    }
}
