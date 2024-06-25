namespace TermTracker.UseCases.UseCaseInterfaces;
public interface IDeleteUseCase<T>
{
    Task ExecuteAsync(int id);
}
