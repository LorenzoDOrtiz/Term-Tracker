using TermTracker.CoreBusiness.Models;

namespace TermTracker.UseCases.Interfaces;

public interface IEditTermUseCase
{
    Task ExecuteAsync(int termId, Term term);

}