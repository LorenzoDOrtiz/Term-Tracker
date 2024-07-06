namespace TermTracker.UseCases.UseCaseInterfaces;

public interface IViewUseCase<T> where T : class
{
    Task<T> ExecuteAsync(int id);
}
