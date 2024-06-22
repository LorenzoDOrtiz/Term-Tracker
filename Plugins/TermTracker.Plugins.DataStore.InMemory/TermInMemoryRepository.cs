using TermTracker.UseCases.PluginInterfaces;
using Term = TermTracker.CoreBusiness.Term;

namespace TermTracker.Plugins.DataStore.InMemory;

// All the code in this file is included in all platforms.
public class TermInMemoryRepository : ITermRepository
{
    public static List<Term> _terms;

    public TermInMemoryRepository()
    {
        _terms = new List<Term>()
        {
            new Term {TermId = 1, TermName = "Term 1", TermStartDate = DateTime.Now, TermEndDate = DateTime.Now.AddMonths(6)},
            new Term {TermId = 2, TermName = "Term 2", TermStartDate = DateTime.Now, TermEndDate = DateTime.Now.AddMonths(7)},
            new Term {TermId = 3, TermName = "Term 3", TermStartDate = DateTime.Now, TermEndDate = DateTime.Now.AddMonths(8)},
            new Term {TermId = 4, TermName = "Term 4", TermStartDate = DateTime.Now, TermEndDate = DateTime.Now.AddMonths(9)},
            new Term {TermId = 5, TermName = "Term 5", TermStartDate = DateTime.Now, TermEndDate = DateTime.Now.AddMonths(10)}
        };
    }
    public Task<List<Term>> GetTermsAsync()
    {
        return Task.FromResult(_terms);
    }
}
