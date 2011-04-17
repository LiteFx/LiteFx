namespace LiteFx.Specification
{
    /// <summary>
    /// Specification interface.
    /// </summary>
    /// <typeparam name="T">Type of the entity that will be verified.</typeparam>
    public interface ISpecification<in T>
    {
        /// <summary>
        /// Verify if the entity passed by parameter will satisfy the specification.
        /// </summary>
        /// <param name="entity">Entity to be verified.</param>
        /// <returns>True if the entity satisfy the specification and false if it not.</returns>
        bool IsSatisfiedBy(T entity);
    }
}