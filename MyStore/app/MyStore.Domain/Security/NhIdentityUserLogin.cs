using SharpLite.Domain;

namespace MyStore.Domain.Security
{
    public class NhIdentityUserLogin: EntityWithTypedId<string>
    {
        public virtual string LoginProvider { get; set; }
        public virtual string ProviderKey { get; set; }
        public virtual string UserId { get; set; }
        
        public virtual NhIdentityUser User { get; set; }
    }
}
