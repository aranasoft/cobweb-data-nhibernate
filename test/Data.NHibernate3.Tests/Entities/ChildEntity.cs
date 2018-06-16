using System;
using System.Collections.Generic;

namespace Cobweb.Data.NHibernate.Tests.Entities {
    public class ChildEntity : Entity<ChildEntity, Guid>, IEquatable<ChildEntity> {
        private IList<GrandchildEntity> _children;
        private IList<RootEntity> _parents;
        public virtual string Name { get; set; }
        public virtual RootEntity Parent { get; set; }

        public virtual IList<RootEntity> Parents {
            get { return _parents ?? (_parents = new List<RootEntity>()); }
            set { _parents = value; }
        }

        public virtual IList<GrandchildEntity> Children {
            get { return _children ?? (_children = new List<GrandchildEntity>()); }
            set { _children = value; }
        }
    }
}