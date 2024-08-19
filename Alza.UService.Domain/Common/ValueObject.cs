﻿namespace Alza.UService.Domain.Common;

/// <summary>
/// A base class for value objects - immutable business objects without identity
/// <see href="https://enterprisecraftsmanship.com/posts/value-objects-explained/" />
/// </summary>
public abstract class ValueObject<T>
    where T : ValueObject<T>
{
    public override bool Equals(object? obj)
    {
        var valueObject = obj as T;

        if (ReferenceEquals(valueObject, null))
            return false;

        return EqualsCore(valueObject);
    }

    protected abstract bool EqualsCore(T other);

    public override int GetHashCode()
    {
        return GetHashCodeCore();
    }

    protected abstract int GetHashCodeCore();

    public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
    {
        return !(a == b);
    }
}
