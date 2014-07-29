using FluentNHibernate.Mapping;
using Sample.Domain;

namespace Sample.Infrastructure.Mappings
{
    public class EntityBaseMap<TEntity> : ClassMap<TEntity>
        where TEntity : EntityBase
    {
        public EntityBaseMap()
        {
            Id(e => e.Id);
        }
    }
}