// Application/Interfaces/IValidator.cs
public interface IValidator<T>
{
    void Validate(T entity);
}