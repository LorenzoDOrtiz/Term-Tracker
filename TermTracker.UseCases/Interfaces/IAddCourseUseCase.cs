using TermTracker.CoreBusiness.Models;

namespace TermTracker.UseCases.Interfaces;
public interface IAddCourseUseCase
{
    Task ExecuteAsync(Course course);
}
