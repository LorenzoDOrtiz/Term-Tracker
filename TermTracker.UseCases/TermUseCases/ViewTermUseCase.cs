using TermTracker.CoreBusiness.Models;
using TermTracker.UseCases.Interfaces;
using TermTracker.UseCases.PluginInterfaces;

namespace TermTracker.UseCases.TermUseCases;
public class ViewTermUseCase : IViewTermUseCase
{
    private readonly ITermRepository termRepository;
    public ViewTermUseCase(ITermRepository termRepository)
    {
        this.termRepository = termRepository;
    }

    public async Task<Term> ExecuteAsync(int termId)
    {
        return await termRepository.GetTermByIdAsync(termId);
    }
}
