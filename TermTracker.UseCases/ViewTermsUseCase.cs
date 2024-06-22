using TermTracker.UseCases.Interfaces;
using TermTracker.UseCases.PluginInterfaces;
using Term = TermTracker.CoreBusiness.Term;

namespace TermTracker.UseCases;

// All the code in this file is included in all platforms.
public class ViewTermsUseCase : IViewTermsUseCase
{
    private readonly ITermRepository termRepository;
    public ViewTermsUseCase(ITermRepository termRepository)
    {
        this.termRepository = termRepository;
    }

    public async Task<List<Term>> ExecuteAsync()
    {
        List<Term> terms = await termRepository.GetTermsAsync();
        return terms;
    }
}
