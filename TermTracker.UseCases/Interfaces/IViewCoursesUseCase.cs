using TermTracker.CoreBusiness.Models;

namespace TermTracker.UseCases.Interfaces;
public interface IViewCoursesUseCase
{
    Task<List<Course>> ExecuteAsync(int termId);
}
