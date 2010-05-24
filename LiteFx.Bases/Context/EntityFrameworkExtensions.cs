using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data;

namespace LiteFx.Bases
{
    /// <summary>
    /// Extensões para o EntityFramework.
    /// </summary>
    public static class EntityFrameworkExtensions
    {
        /// <summary>
        /// Helper provisorio para realizar um update no entity sem precisar de ir ao banco de dados.
        /// </summary>
        /// <param name="context">Contexto do Entity.</param>
        /// <param name="entitySetName">Nome do EntitySet.</param>
        /// <param name="entity">Entity Object.</param>
        public static void Update(this ObjectContext context, string entitySetName, EntityObject entity)
        {
            if (entity.EntityKey == null)
                entity.EntityKey = context.CreateEntityKey(entitySetName, entity);

            if (entity.EntityState == EntityState.Detached)
                context.Attach(entity);

            var stateEntry = context.ObjectStateManager.GetObjectStateEntry(entity.EntityKey);
            var propertyNameList = stateEntry.CurrentValues.DataRecordInfo.FieldMetadata.Select(pn => pn.FieldType.Name);
            foreach (var propName in propertyNameList)
            {
                stateEntry.SetModifiedProperty(propName);
            }

            //var relatedEntities = context.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added).Where(

            
        }

        /// <summary>
        /// Helper provisorio para realizar um update no entity que possuir relacionamentos.
        /// </summary>
        /// <param name="context">Contexto do Entity.</param>
        /// <param name="entitySetName">Nome do EntitySet.</param>
        /// <param name="entity">Entity Object.</param>
        public static void UpdateWithReference(this ObjectContext context, string entitySetName, EntityObject entity)
        {
            if (entity.EntityKey == null)
                entity.EntityKey = context.CreateEntityKey(context.DefaultContainerName + "." + entitySetName, entity);

            if (entity.EntityState == EntityState.Detached)
            {
                object currentEntityInDb;
                if (context.TryGetObjectByKey(entity.EntityKey, out currentEntityInDb))
                {
                    context.ApplyPropertyChanges(entity.EntityKey.EntitySetName, entity);
                    context.ApplyReferencePropertyChanges(entity,
                                                      (IEntityWithRelationships)currentEntityInDb);  //extension
                }
                else
                {
                    throw new ObjectNotFoundException();
                }
            }
        }

        /// <summary>
        /// Atualiza os relacionamentos.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="newEntity"></param>
        /// <param name="oldEntity"></param>
        public static void ApplyReferencePropertyChanges(this ObjectContext context, IEntityWithRelationships newEntity, IEntityWithRelationships oldEntity)
        {
            foreach (var relatedEnd in oldEntity.RelationshipManager.GetAllRelatedEnds())
            {
                var oldRef = relatedEnd as EntityReference;
                if (oldRef == null) continue;
                var newRef = newEntity.RelationshipManager.GetRelatedEnd(oldRef.RelationshipName, oldRef.TargetRoleName) as EntityReference;
                if (newRef != null) oldRef.EntityKey = newRef.EntityKey;
            }
        }
    }
}
