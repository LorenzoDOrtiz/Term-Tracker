using TermTracker.CoreBusiness;

namespace TermTracker.Maui.ViewModels;

public interface IDeleteTermUseCase
{
    Task ExecuteAsync(int termId);
}