using TermTracker.CoreBusiness.Models;

namespace TermTracker.UseCases.PluginInterfaces;
public interface ICourseRepository<T>
{
    Task<List<Course>> GetAllAsync(int termId);
    Task AddAsync(T obj);
    Task<Course> GetByIdAsync(int id);
    Task UpdateAsync(int id, T obj);
    Task DeleteAsync(int id);
}
