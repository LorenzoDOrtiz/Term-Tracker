using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermTracker.CoreBusiness;
using TermTracker.UseCases.Interfaces;
using TermTracker.UseCases.PluginInterfaces;

namespace TermTracker.UseCases;
public class ViewTermUseCase : IViewTermUseCase
{
    private readonly ITermRepository termRepository;
    public ViewTermUseCase(ITermRepository termRepository)
    {
        this.termRepository = termRepository;
    }

    public async Task<Term> ExecuteAsync(int termId)
    {
        return await this.termRepository.GetTermByIdAsync(termId);
    }
}
