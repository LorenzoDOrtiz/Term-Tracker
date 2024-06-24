using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermTracker.CoreBusiness.Models;

namespace TermTracker.UseCases.Interfaces;
public interface IAddTermUseCase
{
    Task ExecuteAsync(Term term);
}
