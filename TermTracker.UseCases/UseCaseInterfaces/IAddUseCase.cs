namespace TermTracker.UseCases.UseCaseInterfaces;
public interface IAddUseCase<T>
{
    Task ExecuteAsync(T obj);
}
