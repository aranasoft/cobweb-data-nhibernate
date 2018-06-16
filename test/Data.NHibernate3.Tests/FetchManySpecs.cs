using System.Collections.Generic;
using System.Linq;
using Cobweb.Data.NHibernate.Tests.Entities;
using Cobweb.Data.NHibernate.Tests.Util;
using FluentAssertions;
using NHibernate;
using NHibernate.Linq;
using Xunit;

namespace Cobweb.Data.NHibernate.Tests {
    public class FetchManySpecs : SqLiteNHibernateTest {
        public FetchManySpecs(SqLiteNHibernateFixture fixture) : base(fixture) {
            var root = new RootEntity {
                Child = new ChildEntity()
            };
            root.Children.Add(root.Child);
            root.Child.Parent = root;
            root.Child.Parents.Add(root);
            var otherRoot = new RootEntity();
            root.Child.Parents.Add(otherRoot);

            using (var tx = Session.BeginTransaction()) {
                Session.Save(root);
                tx.Commit();
            }

            Session.Clear();
            SessionFactory.Statistics.Clear();
        }

        [Fact]
        public void ItShouldHaveTwoRootsFromSetup() {
            Session.Query<RootEntity>().Count().Should().Be(2);
        }

        [Fact]
        public void ItShouldHaveOneChildFromSetup() {
            Session.Query<ChildEntity>().Count().Should().Be(1);
        }

        [Fact]
        public void ItShouldNotInitializeReferenceCollectionsWhenNotFetched() {
            var child = Session.Query<ChildEntity>().FirstOrDefault();
            child.Should().NotBeNull();

            NHibernateUtil.IsInitialized(child.Parents).Should().BeFalse("Parents should not be initialized");
            SessionFactory.Statistics.EntityFetchCount.Should().Be(0, "northing should have been lazy-loaded");
            SessionFactory.Statistics.PrepareStatementCount.Should().Be(1, "there should have been one query");
        }

        [Fact]
        public void ItShouldLazyLoadReferenceCollectionsWhenNotFetched() {
            var child = Session.Query<ChildEntity>().FirstOrDefault();
            child.Should().NotBeNull();

            var parentName = child.Parents.First().Name;

            NHibernateUtil.IsInitialized(child.Parents).Should().BeTrue("Parents should be initialized");
            SessionFactory.Statistics.EntityFetchCount.Should()
                          .Be(0, "the parents should have been loaded through the collection");
            SessionFactory.Statistics.CollectionFetchCount.Should()
                          .Be(1, "the parents collection should have been lazy-loaded");
            SessionFactory.Statistics.PrepareStatementCount.Should().Be(2, "there should have been two queries");
        }

        [Fact]
        public void ItShouldInitializeReferenceCollectionsWhenFetched() {
            var child = Session.Query<ChildEntity>().FetchMany(childEntity => childEntity.Parents).FirstOrDefault();
            child.Should().NotBeNull();

            NHibernateUtil.IsInitialized(child.Parents).Should().BeTrue("Parents should be initialized");
            SessionFactory.Statistics.EntityFetchCount.Should().Be(0, "northing should have been lazy-loaded");
            SessionFactory.Statistics.CollectionFetchCount.Should()
                          .Be(0, "the parents collection should have been eager-loaded");
            SessionFactory.Statistics.PrepareStatementCount.Should().Be(1, "there should have been one query");
        }


        [Fact]
        public void ItShouldEagerLoadReferenceCollectionsWhenFetched() {
            var child = Session.Query<ChildEntity>().FetchMany(childEntity => childEntity.Parents).FirstOrDefault();
            child.Should().NotBeNull();

            var parentName = child.Parents.First().Name;

            NHibernateUtil.IsInitialized(child.Parents).Should().BeTrue("Parents should be initialized");
            SessionFactory.Statistics.EntityFetchCount.Should().Be(0, "the parent should have been eager-loaded");
            SessionFactory.Statistics.CollectionFetchCount.Should()
                          .Be(0, "the parents collection should have been eager-loaded");
            SessionFactory.Statistics.PrepareStatementCount.Should().Be(1, "there should have been one query");
        }
    }
}
