using System;
using System.Collections.Generic;
using MyStore.Domain;
using MyStore.Domain.Security;
using MyStore.NHibernateProvider.Overrides;
using NHibernate.Bytecode;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using SharpLite.NHibernateProvider;
using SharpLite.NHibernateProvider.ConfigurationCaching;

namespace MyStore.NHibernateProvider
{
    public class NHibernateInitializer
    {
        public static Configuration Initialize() {
            INHibernateConfigurationCache cache = new NHibernateConfigurationFileCache();

            var mappingAssemblies = new[] { 
                typeof(Customer).Assembly.GetName().Name
            };

            var configuration = cache.LoadConfiguration(CONFIG_CACHE_KEY, null, mappingAssemblies);

            if (configuration == null) {
                configuration = new Configuration();

                configuration
                    .Proxy(p => p.ProxyFactoryFactory<DefaultProxyFactoryFactory>())
                    .DataBaseIntegration(db => {
                        db.ConnectionStringName = "MyStoreConnectionString";
                        db.Dialect<MsSql2008Dialect>();
                    })
                    .AddAssembly(typeof(Customer).Assembly)
                    .CurrentSessionContext<LazySessionContext>();

                var mapper = new ConventionModelMapper();

                mapper.AddMappings(new List<Type>()
                {
                    typeof (NhIdentityUserMapping),
                    typeof (NhIdentityUserRoleMapping),
                    typeof (NhNhIdentityUserLoginMapping),
                    typeof (NhIdentityUserClaimMapping)
                });
                var mapping = mapper.CompileMappingFor(new List<Type>()
                {
                    typeof (NhIdentityUser),
                    typeof (NhIdentityUserRole),
                    typeof (NhIdentityUserLogin),
                    typeof (NhIdentityUserClaim)
                });
                var lastCompiledXml = mapping.AsString();
                configuration.AddDeserializedMapping(mapping, "IdentityMappings");

                //mapper.WithConventions(configuration);
                
                cache.SaveConfiguration(CONFIG_CACHE_KEY, configuration);
            }

            return configuration;
        }

        private const string CONFIG_CACHE_KEY = "MyStore";
    }
}