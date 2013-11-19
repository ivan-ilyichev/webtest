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
    public class UserClaimMapping : ClassMapping<UserClaim>
    {
        public UserClaimMapping()
        {
            Table("AspNetUserClaims");

            ManyToOne<User>(l => l.User, a => a.Column("User_Id"));
        }
    }
}
