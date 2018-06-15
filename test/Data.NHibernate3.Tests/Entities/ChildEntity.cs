using System;
using System.Collections.Generic;

namespace Cobweb.Data.NHibernate.Tests.Entities {
    public class ChildEntity : Entity<ChildEntity, Guid>, IEquatable<ChildEntity> {
        public virtual string Name { get; set; }
        public virtual RootEntity Parent { get; set; }
        public virtual IList<RootEntity> Parents { get; set; }
        public virtual IList<GrandchildEntity> Children { get; set; }
    }
}