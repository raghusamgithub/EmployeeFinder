
using System;
using System.Collections.Generic;
using System.Text;
using AWE.Employee.Common;

namespace AWE.Employee.Common.Utilities
{
    public static class MappingExtensions
    {
        #region Utilities

        /// <summary>
        /// Execute a mapping from the source object to a new destination object. The source type is inferred from the source object
        /// </summary>
        /// <typeparam name="TDestination">Destination object type</typeparam>
        /// <param name="source">Source object to map from</param>
        /// <returns>Mapped destination object</returns>
        private static TDestination Map<TDestination>(this object source)
        {
            //use AutoMapper for mapping objects
            return AutoMapperConfiguration.Mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// Execute a mapping from the source object to the existing destination object
        /// </summary>
        /// <typeparam name="TSource">Source object type</typeparam>
        /// <typeparam name="TDestination">Destination object type</typeparam>
        /// <param name="source">Source object to map from</param>
        /// <param name="destination">Destination object to map into</param>
        /// <returns>Mapped destination object, same instance as the passed destination object</returns>
        private static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            //use AutoMapper for mapping objects
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }

        #endregion

        #region Methods

        #region Model-Entity mapping

        /// <summary>
        /// Execute a mapping from the entity to a new model
        /// </summary>
        /// <typeparam name="TModel">Model type</typeparam>
        /// <param name="baseModel">Entity to map from</param>
        /// <returns>Mapped model</returns>
        public static TModel ToDTO<TModel>(this BaseModel baseModel) where TModel : BaseDTO
        {
            if (baseModel == null)
                return null;

            return baseModel.Map<TModel>();
        }

        public static List<TDTO> ToModelDTO<TModel, TDTO>(this List<TModel> baseDTO)
            where TModel : BaseModel
            where TDTO : BaseDTO
        {
            if (baseDTO == null)
                return null;

            return baseDTO.Map<List<TDTO>>();
        }

        public static List<TModel> ToDTOModel<TDTO, TModel>(this List<TDTO> baseDTO)
            where TModel : BaseModel
            where TDTO : BaseDTO
        {
            if (baseDTO == null)
                return null;

            return baseDTO.Map<List<TModel>>();
        }

        public static TDTO ToDTO<TDTO>(this BaseEntity baseModel) where TDTO : BaseDTO
        {
            if (baseModel == null)
                return null;

            return baseModel.Map<TDTO>();
        }

        public static List<TDTO> ToEntityDTO<TEntity, TDTO>(this List<TEntity> baseModel)
            where TDTO : BaseDTO
            where TEntity : BaseEntity
        {
            if (baseModel == null)
                return null;

            return baseModel.Map<List<TDTO>>();
        }

        public static TModel ToModel<TModel>(this BaseDTO baseDTO) where TModel : BaseModel
        {
            if (baseDTO == null)
                return null;

            return baseDTO.Map<TModel>();
        }

        public static List<TModel> ToModel<TDTO, TModel>(this List<TDTO> baseDTO) where TModel : BaseModel where TDTO : BaseDTO
        {
            if (baseDTO == null)
                return null;

            return baseDTO.Map<List<TModel>>();
        }

        /// <summary>
        /// Execute a mapping from the model to a new entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="model">Model to map from</param>
        /// <returns>Mapped entity</returns>
        public static TEntity ToEntity<TEntity>(this BaseDTO model) where TEntity : BaseEntity
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return model.Map<TEntity>();
        }

        /// <summary>
        /// Execute a mapping from the model to the existing entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="TModel">Model type</typeparam>
        /// <param name="model">Model to map from</param>
        /// <param name="entity">Entity to map into</param>
        /// <returns>Mapped entity</returns>
        public static TEntity ToEntity<TModel, TEntity>(this TModel model, TEntity entity)
            where TEntity : BaseEntity where TModel : BaseDTO
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return model.MapTo(entity);
        }

        public static TModel ToDTO<TEntity, TModel>(this TEntity entity, TModel model)
            where TEntity : BaseEntity where TModel : BaseDTO
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (entity == null)
            {
                return model;
            }
            //throw new ArgumentNullException(nameof(entity));

            return entity.MapTo(model);
        }

        public static List<TEntity> ToEntity<TDTO, TEntity>(this List<TDTO> model)
            where TEntity : BaseEntity where TDTO : BaseDTO
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return model.Map<List<TEntity>>();
        }
        #endregion

        #endregion
    }
}
