using Autofac;
using MyERP.Core.Repositories;
using MyERP.Core.Services;
using MyERP.Core.UnitOfWorks;
using MyERP.Repository;
using MyERP.Repository.Repositories;
using MyERP.Repository.UnitOfWorks;
using MyERP.Service.Mappings;
using MyERP.Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace MyERP.API.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Generic repository and service registration
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericService<>)).As(typeof(IGenericService<>)).InstancePerLifetimeScope();  // Corrected this line

            // UnitOfWork registration
            builder.RegisterType<UnitOfWorks>().As<IUnitOfWorks>().InstancePerLifetimeScope();

            builder.RegisterType<TokenHandler>().As<ITokenHandler>().InstancePerLifetimeScope();

            // Assemblies for repositories and services
            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            // Register all repositories
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // Register all services
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
