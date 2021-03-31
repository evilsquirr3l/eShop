using Data.Entities;

namespace Business.Abstraction
{
    public interface IServiceHelper<in TEntity> where TEntity : BaseEntity
    {
        void ThrowValidationExceptionIfModelIsNull(TEntity entity);
    }
}