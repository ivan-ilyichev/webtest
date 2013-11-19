using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpLite.Domain;

namespace MyStore.Domain.Security
{
    public class UserClaim : EntityWithTypedId<string>
    {
        public UserClaim()
        {
            Id = Guid.NewGuid().ToString();
        }

        public virtual string ClaimType { get; set; }

        public virtual string ClaimValue { get; set; }

        public virtual User User { get; set; }
    }
}
