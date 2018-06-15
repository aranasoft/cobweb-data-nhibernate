using System.Collections.Generic;
using System.Reflection;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Helpers;
using NHibernate.Cfg;

namespace Cobweb.Data.NHibernate.Tests.Util {
    public class SqLiteNHibernateFixture {
        public Configuration SessionConfiguration { get; private set; }

        public SqLiteNHibernateFixture() {
            var connectionConfig = SQLiteConfiguration.Standard.InMemory().QuerySubstitutions("true=1;false=0");
            FluentNHibernate.Cfg.Fluently.Configure()
                            .Mappings(
                                m =>
                                    m.AutoMappings.Add(
                                        AutoMap.Source(GetEntityTypeSources())
                                               .Conventions.Setup(ConfigureConventions)))
                            .ExposeConfiguration(config => { SessionConfiguration = config; })
                            .Database(connectionConfig)
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
