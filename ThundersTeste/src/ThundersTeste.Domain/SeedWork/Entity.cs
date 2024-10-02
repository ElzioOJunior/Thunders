using System.Linq.Expressions;
using System.Xml.Linq;
using MediatR;
using System;

namespace ThundersTeste.Domain.SeedWork;

public abstract class Entity
{
    public Guid Id { get; private set; }

    protected void SetId(Guid id = default)
    {
        Id = id.Equals(Guid.Empty) ? Guid.NewGuid() : id;
    }

    protected static void ValidateField<TEntity, TValue>(Expression<Func<TEntity, TValue>> field, bool validation, string errorMessage = null)
    {
        if (validation)
            throw new Exception(errorMessage ?? GetDefaultValidationErrorMessage(field));
    }

    private static string GetDefaultValidationErrorMessage<TEntity, TValue>(Expression<Func<TEntity, TValue>> field)
    {
        var expression = (MemberExpression)field.Body;
        string name = expression.Member.Name;
        var defaultMessage = $"{name} deve ser informado!";

        return defaultMessage;
    }
}
