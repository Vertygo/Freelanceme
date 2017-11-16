using Autofac;
using Autofac.Extensions.DependencyInjection;
using Freelanceme.Data.EntityFramework;
using System;
using Microsoft.Extensions.DependencyInjection;
using Freelanceme.Security;
using Freelanceme.Data;
using Freelanceme.Domain.Core;
using Freelancme.WebApi.V1.Services;
using Freelancme.WebApi.V1.Services.Interfaces;

namespace Freelancme.WebApi
{
    internal static class Bootstrap
    {
        internal static IServiceProvider InitializeContainer(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();
            builder.RegisterType<AuthManager>().As<IAuthManager>().InstancePerDependency();
            builder.RegisterType<UserManager>().As<IUserManager>().InstancePerDependency();
            builder.RegisterType<ApplicationDbContext>().As<IDbContext>().InstancePerDependency();
            builder.RegisterType<AuthService>().As<IAuthService>().InstancePerDependency();
            builder.RegisterType<ClientService>().As<IClientService>().InstancePerDependency();
            builder.RegisterType<TimeTrackingService>().As<ITimeTrackingService>().InstancePerDependency();

            builder.Populate(services);

            return new AutofacServiceProvider(builder.Build());
        }
        
    }
}