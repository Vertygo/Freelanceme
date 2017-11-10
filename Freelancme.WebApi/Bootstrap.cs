using Autofac;
using Autofac.Extensions.DependencyInjection;
using Freelanceme.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;
using Freelanceme.Security;
using Microsoft.AspNetCore.Identity;
using Freelanceme.Data;
using Freelanceme.Domain.Core;

namespace Freelancme.WebApi
{
    internal static class Bootstrap
    {
        internal static IServiceProvider InitializeContainer(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            //builder.RegisterModule<DataModule>();
            //builder.RegisterType<SeedDataService>().As<ISeedDataService>();

            //builder.Populate(services);
            //var hybridLifestyle = Lifestyle.CreateHybrid(new AsyncScopedLifestyle(), new ThreadScopedLifestyle());

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();
            builder.RegisterType<AuthManager>().As<IAuthManager>().InstancePerDependency();
            builder.RegisterType<UserManager>().As<IUserManager>().InstancePerDependency();


            builder.RegisterType<ApplicationDbContext>().As<IDbContext>().InstancePerDependency();
            builder.RegisterType<ApplicationDbContext>().As<DbContext>().InstancePerDependency();
            builder.Populate(services);

            return new AutofacServiceProvider(builder.Build());
        }
        
    }
}