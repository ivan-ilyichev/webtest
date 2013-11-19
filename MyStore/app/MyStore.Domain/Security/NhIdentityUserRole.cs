using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SharpLite.Domain;

namespace MyStore.Domain.Security
{
    public class NhIdentityUserRole : EntityWithTypedId<string>, IRole
    {
        public virtual string Name { get; set; }
    }
}
