namespace TermTracker.UseCases.UseCaseInterfaces
{
    public interface IViewCollectionUseCase<T> where T : class
    {
        Task<List<T>> ExecuteAsync(int termId);
        Task<List<T>> ExecuteAsync();
    }
}
