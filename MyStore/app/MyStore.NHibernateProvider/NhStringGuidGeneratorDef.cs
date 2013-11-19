using NHibernate.Mapping.ByCode;
using System;

namespace MyStore.NHibernateProvider
{
    public class NhStringGuidGeneratorDef : IGeneratorDef
    {
        public string Class
        {
            get { return typeof(NhStringGuidGenerator).AssemblyQualifiedName; }
        }

        public object Params
        {
            get { return null; }
        }

        public Type DefaultReturnType
        {
            get { return typeof(string); }
        }

        public bool SupportedAsCollectionElementId
        {
            get { return true; }
        }
    }
}
