using Term = TermTracker.CoreBusiness.Term;

namespace TermTracker.UseCases.PluginInterfaces;
public interface ITermRepository
{
    Task<List<Term>> GetTermsAsync();

}
