using System;
using System.Collections.Generic;

namespace Cobweb.Data.NHibernate.Tests.Entities {
    public class RootEntity : Entity<RootEntity, Guid>, IEquatable<RootEntity> {
        public virtual string Name { get; set; }
        public virtual ChildEntity Child { get; set; }
        public virtual IList<ChildEntity> Children { get; set; }
    }
}