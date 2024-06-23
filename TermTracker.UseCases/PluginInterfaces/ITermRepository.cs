using Term = TermTracker.CoreBusiness.Term;

namespace TermTracker.UseCases.PluginInterfaces;
public interface ITermRepository
{
    Task<List<Term>> GetTermsAsync();
    Task<Term> GetTermByIdAsync(int termId);
    Task AddTermAsync(Term term);
    Task DeleteTermAsync(int termId);
    Task UpdateTermAsync(int termId, Term term); 
}
