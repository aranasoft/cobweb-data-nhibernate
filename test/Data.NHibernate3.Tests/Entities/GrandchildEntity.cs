using System;
using System.Collections.Generic;

namespace Cobweb.Data.NHibernate.Tests.Entities {
    public class GrandchildEntity : Entity<GrandchildEntity, Guid>, IEquatable<GrandchildEntity> {
        private IList<ChildEntity> _parents;
        public virtual string Name { get; set; }
        public virtual ChildEntity Parent { get; set; }

        public virtual IList<ChildEntity> Parents {
            get { return _parents ?? (_parents = new List<ChildEntity>()); }
            set { _parents = value; }
        }
    }
}