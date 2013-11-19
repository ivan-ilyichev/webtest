using MyStore.Domain;
using MyStore.Domain.Security;
using NHibernate.Mapping.ByCode.Conformist;

namespace MyStore.NHibernateProvider.Overrides
{
    public class NhNhIdentityUserLoginMapping : ClassMapping<NhIdentityUserLogin>
    {
        public NhNhIdentityUserLoginMapping()
        {
            Table("AspNetUserLogins");

            ComposedId(m =>
            {
                m.ManyToOne<NhIdentityUser>(x => x.User, a => a.Column("UserId"));
                m.Property(x => x.LoginProvider);
                m.Property(x => x.ProviderKey);
            });
            //ManyToOne<NhIdentityUser>(l => l.User, a => a.Column("UserId"));
        }
    }
}
