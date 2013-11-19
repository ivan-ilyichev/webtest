using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using MyStore.Domain;
using MyStore.Domain.Security;
using NHibernate.Mapping.ByCode;

namespace MyStore.NHibernateProvider.Overrides
{
    /// <summary>
    /// Overrides the class-conventions for the Order object
    /// </summary>
    public class UserOverride : IOverride
    {
        public void Override(ModelMapper mapper) {
            // AspNetUserRoles
        }
    }
}
