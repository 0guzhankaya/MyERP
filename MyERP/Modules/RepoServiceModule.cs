using Autofac;
using MyERP.Core.Repositories;
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
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerMatchingLifetimeScope();
            builder.RegisterGeneric(typeof(GenericService<>)).As(typeof(IGenericRepository<>)).InstancePerMatchingLifetimeScope();
            builder.RegisterType<UnitOfWorks>().As<IUnitOfWorks>();

            // register type token handler
            
            var  apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
