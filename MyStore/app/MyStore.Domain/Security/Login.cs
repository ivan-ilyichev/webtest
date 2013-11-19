using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpLite.Domain;

namespace MyStore.Domain.Security
{
    public class Login: EntityWithTypedId<string>
    {
        public virtual string LoginProvider { get; set; }
        public virtual string ProviderKey { get; set; }
        public virtual string UserId { get; set; }
        
        public virtual User User { get; set; }
    }
}
