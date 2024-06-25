using TermTracker.CoreBusiness.Models;

namespace TermTracker.UseCases.PluginInterfaces;
public interface ITermRepository<T>
{
    Task<List<Term>> GetAllAsync();
    Task AddAsync(T obj);
    Task<Term> GetByIdAsync(int id);
    Task UpdateAsync(int id, T obj);
    Task DeleteAsync(int id);
}
