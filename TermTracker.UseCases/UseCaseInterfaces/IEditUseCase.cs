namespace TermTracker.UseCases.UseCaseInterfaces;
public interface IEditUseCase<T>
{
    Task ExecuteAsync(int id, T obj);
}
