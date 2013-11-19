using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using SharpLite.Domain;

namespace MyStore.Domain.Security
{
    public class NhIdentityUserLogin
    {
        public virtual string LoginProvider { get; set; }
        public virtual string ProviderKey { get; set; }
        
        public virtual NhIdentityUser User { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var compareTo = obj as NhIdentityUserLogin;
            if (compareTo == null)
                return false;

            return compareTo.User.Equals(this.User) &&
                   compareTo.LoginProvider == this.LoginProvider &&
                   compareTo.ProviderKey == this.ProviderKey;
        }

        public override int GetHashCode()
        {
            return (User != null ? User.GetHashCode().ToString(CultureInfo.InvariantCulture) : "null" + 
                    "|" + LoginProvider + 
                    "|" + ProviderKey
                ).GetHashCode();
        }
    }
}
