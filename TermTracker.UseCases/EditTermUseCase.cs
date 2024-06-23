using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermTracker.CoreBusiness;
using TermTracker.UseCases.Interfaces;
using TermTracker.UseCases.PluginInterfaces;

namespace TermTracker.UseCases;
public class EditTermUseCase : IEditTermUseCase
{
    private readonly ITermRepository termRepository;

    public EditTermUseCase(ITermRepository termRepository)
    {
        this.termRepository = termRepository;
    }

    public async Task ExecuteAsync(int termId, Term term)
    {
        await this.termRepository.UpdateTermAsync(termId, term); 
    }
}
