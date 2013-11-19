using MyStore.Domain;
using MyStore.Domain.Security;
using NHibernate.Mapping.ByCode.Conformist;

namespace MyStore.NHibernateProvider.Overrides
{
    public class NhIdentityUserClaimMapping : ClassMapping<NhIdentityUserClaim>
    {
        public NhIdentityUserClaimMapping()
        {
            Table("AspNetUserClaims");

            Id(c => c.Id);

            ManyToOne<NhIdentityUser>(l => l.User, a => a.Column("User_Id"));
        }
    }
}
