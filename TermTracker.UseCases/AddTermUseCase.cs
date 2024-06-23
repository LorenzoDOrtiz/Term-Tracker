using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermTracker.CoreBusiness;
using TermTracker.UseCases.Interfaces;
using TermTracker.UseCases.PluginInterfaces;

namespace TermTracker.UseCases;
public class AddTermUseCase : IAddTermUseCase
{
    private readonly ITermRepository termRepository;

    public AddTermUseCase(ITermRepository termRepository)
    {
        this.termRepository = termRepository;
    }

    public async Task ExecuteAsync(Term term)
    {
        await this.termRepository.AddTermAsync(term);
    }
}
