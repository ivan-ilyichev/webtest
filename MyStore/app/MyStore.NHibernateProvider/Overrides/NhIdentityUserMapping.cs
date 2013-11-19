using MyStore.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Cascade = NHibernate.Mapping.ByCode.Cascade;

namespace MyStore.NHibernateProvider.Overrides
{
    public class NhIdentityUserMapping : ClassMapping<NhIdentityUser>
    {
        public NhIdentityUserMapping()
        {
            Table("AspNetUsers");
            Id(x => x.Id, m => m.Generator(new NhStringGuidGeneratorDef()));

            Bag(e => e.Claims, bag =>
            {
                bag.Inverse(true);
                bag.Cascade(Cascade.All | Cascade.DeleteOrphans);
                bag.Key(k => k.Column("User_Id"));
                bag.Lazy(CollectionLazy.Lazy);
            });

            Bag(e => e.Logins, bag =>
            {
                bag.Inverse(true);
                bag.Cascade(Cascade.All | Cascade.DeleteOrphans);
                bag.Key(k => k.Column("UserId"));
                bag.Lazy(CollectionLazy.Lazy);
            });

            Bag(e => e.Roles, 
                bag => 
                {
                    bag.Inverse(true);
                    bag.Cascade(Cascade.All | Cascade.DeleteOrphans);

                    bag.Table("AspNetUserRoles");
                    bag.Key(k => k.Column("UserId"));
                    bag.Lazy(CollectionLazy.Lazy);
                }, 
                r => r.ManyToMany(m =>
                {
                    m.Column("RoleId");
                })
            );
        }
    }
}
