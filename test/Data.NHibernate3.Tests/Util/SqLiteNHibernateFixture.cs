using System.Collections.Generic;
using System.Reflection;
using Cobweb.Testing.NHibernate;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Helpers;
using NHibernate.Cfg;

namespace Cobweb.Data.NHibernate.Tests.Util {
    public class SqLiteNHibernateFixture {
        public Configuration SessionConfiguration { get; private set; }

        public SqLiteNHibernateFixture() {
            var connectionConfig = new FluentMigratorSQLiteInMemoryConnectionConfiguration();
            FluentNHibernate.Cfg.Fluently.Configure()
                            .Mappings(
                                m =>
                                    m.AutoMappings.Add(
                                        AutoMap.Source(GetEntityTypeSources())
                                               .Conventions.Setup(ConfigureConventions)))
                            .ExposeConfiguration(config => { SessionConfiguration = config; })
                            .Database(connectionConfig.Configuration())
                            .BuildConfiguration();
        }

        private static void ConfigureConventions(IConventionFinder conventions) {
            conventions.Add(DefaultCascade.SaveUpdate());
            conventions.Add(DefaultLazy.Always());
            conventions.Add(ConventionBuilder.Id.Always(convention => convention.GeneratedBy.GuidComb()));
            conventions.Add(ConventionBuilder.HasMany.Always(convention => convention.Inverse()));
            conventions.Add(ConventionBuilder.HasMany.Always(convention => convention.Cascade.AllDeleteOrphan()));
        }

        private ITypeSource GetEntityTypeSources() {
            var assemblies = new List<Assembly> {GetType().Assembly};
            return new EntityTypeSource(assemblies);
        }
    }
}
