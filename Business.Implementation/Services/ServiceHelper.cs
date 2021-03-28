using Business.Abstraction;
using Data.Entities;

namespace Business.Implementation.Services
{
    public class ServiceHelper<TEntity> : IServiceHelper<TEntity> where TEntity: BaseEntity
    {
        public void ThrowValidationExceptionIfModelIsNull(TEntity entity)
        {
            if (entity == null)
            {
                throw new ValidationException($"{nameof(entity)} is null.");
            }
        }
    }
}