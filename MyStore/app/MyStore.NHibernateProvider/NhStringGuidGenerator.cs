using NHibernate.Engine;
using NHibernate.Id;
using System;

namespace MyStore.NHibernateProvider
{
    public class NhStringGuidGenerator : IIdentifierGenerator
    {
        public object Generate(ISessionImplementor session, object obj)
        {
            return Guid.NewGuid().ToString();
        }
    }
}
