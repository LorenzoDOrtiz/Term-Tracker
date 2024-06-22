using TermTracker.CoreBusiness;

namespace TermTracker.UseCases.Interfaces;
public interface IViewTermsUseCase
{
    Task<List<Term>> ExecuteAsync();
}