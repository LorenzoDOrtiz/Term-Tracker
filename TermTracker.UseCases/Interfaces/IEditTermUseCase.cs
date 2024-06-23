using TermTracker.CoreBusiness;

namespace TermTracker.UseCases.Interfaces;

public interface IEditTermUseCase
{
    Task ExecuteAsync(int termId, Term term);

}