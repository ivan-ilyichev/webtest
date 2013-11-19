using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyStore.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MyStore.NHibernateProvider.Overrides
{
    public class UserMapping : ClassMapping<User>
    {
        public UserMapping()
        {
            Table("AspNetUsers");
            Id(u => u.Id);

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
            /*
            HasManyToMany(e => e.Wards)
                .AsBag()
                .Table("erep_ReportWards")
                .ParentKeyColumn("ReportId")
                .ChildKeyColumn("WardId")
                .LazyLoad()
                .Cascade.All();             
             */
        }
    }
}
