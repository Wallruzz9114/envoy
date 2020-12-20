using FluentValidation;
using FluentValidation.Results;

namespace Models.Abstractions
{
    public abstract class Entity<TId> where TId : struct
    {
        public TId Id { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid => ValidationResult?.IsValid ?? default;

        protected void Validate<TEntity>(TEntity entity, AbstractValidator<TEntity> validator) =>
            ValidationResult = validator.Validate(entity);
    }
}