using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyStore.Domain;
using MyStore.Domain.Security;
using NHibernate.Mapping.ByCode.Conformist;

namespace MyStore.NHibernateProvider.Overrides
{
    public class LoginMapping : ClassMapping<Login>
    {
        public LoginMapping()
        {
            Table("AspNetUserLogins");

            ComposedId(m =>
            {
                m.Property(x => x.UserId);
                m.Property(x => x.LoginProvider);
                m.Property(x => x.ProviderKey);
            });
            ManyToOne<User>(l => l.User, a => a.Column("UserId"));
        }
    }
}
