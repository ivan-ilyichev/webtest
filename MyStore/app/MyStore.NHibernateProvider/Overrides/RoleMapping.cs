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
    public class RoleMapping : ClassMapping<Role>
    {
        public RoleMapping()
        {
            Table("AspNetRoles");

            Id(r => r.Id);
            Property(r => r.Name);
        }
    }
}
