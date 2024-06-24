using TermTracker.CoreBusiness.Models;

namespace TermTracker.UseCases.PluginInterfaces;
public interface ICourseRepository
{
    Task<List<Course>> GetCoursesAsync(int termId);
    //Task<Course> GetTermByIdAsync(int termId);
    Task AddCourseAsync(Course course);
    //Task DeleteTermAsync(int termId);
    //Task UpdateTermAsync(int termId, Course term);
}
