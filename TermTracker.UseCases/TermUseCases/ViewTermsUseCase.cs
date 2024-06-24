using TermTracker.CoreBusiness.Models;
using TermTracker.UseCases.Interfaces;
using TermTracker.UseCases.PluginInterfaces;

namespace TermTracker.UseCases.TermUseCases;

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
