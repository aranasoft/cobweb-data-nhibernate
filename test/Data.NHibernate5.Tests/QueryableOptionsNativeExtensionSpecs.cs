using System;
using System.Linq;
using Cobweb.Data.NHibernate.Tests.Entities;
using FluentAssertions;
using NHibernate;
using NHibernate.Linq;
using Xunit;

namespace Cobweb.Data.NHibernate.Tests {
    [Collection("QueryableOptionsProvider")]
    public class QueryableOptionsNativeExtensionSpecs {
        [Fact]
        public void ItShouldThrowOnCacheableWithDirectCacheableCall() {
            Action act = () => LinqExtensionMethods.Cacheable(Enumerable.Empty<PersonEntity>().AsQueryable()).FirstOrDefault();

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }

        [Fact]
        public void ItShouldThrowOnCacheModeWithDirectCacheModeCall() {
            Action act = () => LinqExtensionMethods.CacheMode(Enumerable.Empty<PersonEntity>().AsQueryable(), CacheMode.Normal).FirstOrDefault();

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }

        [Fact]
        public void ItShouldThrowOnCacheRegionWithDirectCacheRegionCall() {
            Action act = () => LinqExtensionMethods.CacheRegion(Enumerable.Empty<PersonEntity>().AsQueryable(), "test").FirstOrDefault();

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }

        [Fact]
        public void ItShouldThrowOnTimeoutWithDirectTimeoutCall() {
            Action act = () => LinqExtensionMethods.Timeout(Enumerable.Empty<PersonEntity>().AsQueryable(), 1000).FirstOrDefault();

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }

        [Fact]
        public void ItShouldThrowOnCacheableWithDirectSetOptionsCall() {
            Action act = () => LinqExtensionMethods.SetOptions(Enumerable.Empty<PersonEntity>().AsQueryable(), options => options.SetCacheable(true)).FirstOrDefault();

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }

        [Fact]
        public void ItShouldThrowOnCacheableWithDirectWithOptionsCall() {
            Action act = () => LinqExtensionMethods.WithOptions(Enumerable.Empty<PersonEntity>().AsQueryable(), options => options.SetCacheable(true)).FirstOrDefault();

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }
    }
}
