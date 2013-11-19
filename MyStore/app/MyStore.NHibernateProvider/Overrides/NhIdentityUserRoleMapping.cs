using MyStore.Domain.Security;
using NHibernate.Mapping.ByCode.Conformist;

namespace MyStore.NHibernateProvider.Overrides
{
    public class NhIdentityUserRoleMapping : ClassMapping<NhIdentityUserRole>
    {
        public NhIdentityUserRoleMapping()
        {
            Table("AspNetRoles");

            Id(x => x.Id, m => m.Generator(new NhStringGuidGeneratorDef()));
            Property(r => r.Name);
        }
    }
}
