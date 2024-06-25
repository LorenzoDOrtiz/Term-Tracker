using TermTracker.UseCases.PluginInterfaces;
using Term = TermTracker.CoreBusiness.Models.Term;

namespace TermTracker.Plugins.DataStore.InMemory;
public class TermInMemoryRepository : ITermRepository<Term>
{
    public List<Term> _terms;

    public TermInMemoryRepository()
    {
        _terms = new List<Term>()
        {
            new Term {TermId = 1, TermName = "Term 1", TermStartDate = DateTime.Now, TermEndDate = DateTime.Now.AddMonths(6)},
            new Term {TermId = 2, TermName = "Term 2", TermStartDate = DateTime.Now.AddMonths(12), TermEndDate = DateTime.Now.AddMonths(18)},
            new Term {TermId = 3, TermName = "Term 3", TermStartDate = DateTime.Now.AddMonths(18), TermEndDate = DateTime.Now.AddMonths(24)},
            new Term {TermId = 4, TermName = "Term 4", TermStartDate = DateTime.Now.AddMonths(24), TermEndDate = DateTime.Now.AddMonths(30)},
            new Term {TermId = 5, TermName = "Term 5", TermStartDate = DateTime.Now.AddMonths(30), TermEndDate = DateTime.Now.AddMonths(36)}
        };
    }
    public Task<List<Term>> GetAllAsync()
    {
        return Task.FromResult(_terms);
    }

    public Task AddAsync(Term term)
    {
        var maxId = _terms.Max(x => x.TermId);
        term.TermId = maxId + 1;
        _terms.Add(term);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int termId)
    {
        var term = _terms.FirstOrDefault(x => x.TermId == termId);
        if (term != null)
        {
            _terms.Remove(term);
        }

        return Task.CompletedTask;
    }

    public Task<Term> GetByIdAsync(int termId)
    {
        var term = _terms.FirstOrDefault(x => x.TermId == termId);
        if (term != null)
        {
            return Task.FromResult(new Term
            {
                TermId = termId,
                TermName = term.TermName,
                TermStartDate = term.TermStartDate,
                TermEndDate = term.TermEndDate
            });
        }
        return Task.FromResult<Term>(null);
    }

    public Task UpdateAsync(int termId, Term term)
    {
        if (termId != term.TermId) return Task.CompletedTask;

        var termToUpdate = _terms.FirstOrDefault(x => x.TermId == termId);
        if (termToUpdate != null)
        {
            termToUpdate.TermId = termId;
            termToUpdate.TermName = term.TermName;
            termToUpdate.TermStartDate = term.TermStartDate;
            termToUpdate.TermEndDate = term.TermEndDate;
        }

        return Task.CompletedTask;
    }
}
