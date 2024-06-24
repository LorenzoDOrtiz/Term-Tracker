using TermTracker.CoreBusiness.Models;
using TermTracker.UseCases.Interfaces;
using TermTracker.UseCases.PluginInterfaces;

namespace TermTracker.UseCases.TermUseCases;
public class AddTermUseCase : IAddTermUseCase
{
    private readonly ITermRepository termRepository;

    public AddTermUseCase(ITermRepository termRepository)
    {
        this.termRepository = termRepository;
    }

    public async Task ExecuteAsync(Term term)
    {
        await termRepository.AddTermAsync(term);
    }
}
