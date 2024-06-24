using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermTracker.Maui.ViewModels;
using TermTracker.UseCases.PluginInterfaces;

namespace TermTracker.UseCases.TermUseCases;
public class DeleteTermUseCase : IDeleteTermUseCase
{
    private readonly ITermRepository termRepository;

    public DeleteTermUseCase(ITermRepository termRepository)
    {
        this.termRepository = termRepository;
    }

    public async Task ExecuteAsync(int termId)
    {
        await termRepository.DeleteTermAsync(termId);
    }
}
