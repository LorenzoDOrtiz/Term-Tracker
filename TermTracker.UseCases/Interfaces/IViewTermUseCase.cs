using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermTracker.CoreBusiness;

namespace TermTracker.UseCases.Interfaces;
public interface IViewTermUseCase
{
    Task<Term> ExecuteAsync(int termId);
}
