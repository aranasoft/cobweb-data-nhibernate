using System;
using System.Linq;
using Cobweb.Data.NHibernate.Providers;
using Cobweb.Data.NHibernate.QueryableOptions;
using Cobweb.Data.NHibernate.Tests.Entities;
using FluentAssertions;
using NHibernate;
using Xunit;

namespace Cobweb.Data.NHibernate.Tests {
    [Collection("QueryableOptionsProvider")]
    public class QueryableOptionsDefaultProviderSpecs {
        [Fact]
        public void ItShouldUseTheDefaultQueryableOptionsProviderWhenSet() {
            QueryableOptionsProvider.Current().Should().BeOfType<NHibernateQueryableOptionsProvider>();
        }
 
        [Fact]
        public void ItShouldThrowOnCacheableWithQueryableOptionsProviderCacheableCall() {
            Action act = () => QueryableOptionsProvider.Cacheable(Enumerable.Empty<PersonEntity>().AsQueryable()).FirstOrDefault();

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }

        [Fact]
        public void ItShouldThrowOnCacheModeWithDirectCacheModeCall() {
            Action act = () => QueryableOptionsProvider.CacheMode(Enumerable.Empty<PersonEntity>().AsQueryable(), CacheMode.Normal).FirstOrDefault();

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }

        [Fact]
        public void ItShouldThrowOnCacheRegionWithDirectCacheRegionCall() {
            Action act = () => QueryableOptionsProvider.CacheRegion(Enumerable.Empty<PersonEntity>().AsQueryable(), "test").FirstOrDefault();

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }

        [Fact]
        public void ItShouldThrowOnTimeoutWithDirectTimeoutCall() {
            Action act = () => QueryableOptionsProvider.Timeout(Enumerable.Empty<PersonEntity>().AsQueryable(), 1000).FirstOrDefault();

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }

        [Fact]
        public void ItShouldThrowOnCacheableWithQueryableOptionsProviderSetOptionsCall() {
            Action act = () => QueryableOptionsProvider.SetOptions(Enumerable.Empty<PersonEntity>().AsQueryable(), options => options.SetCacheable(true)).FirstOrDefault();

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }

        [Fact]
        public void ItShouldThrowOnCacheableWithQueryableOptionsProviderWithOptionsCall() {
            Action act = () => QueryableOptionsProvider.WithOptions(Enumerable.Empty<PersonEntity>().AsQueryable(), options => options.SetCacheable(true)).FirstOrDefault();

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }
    }
}
