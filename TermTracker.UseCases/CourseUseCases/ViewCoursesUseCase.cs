using TermTracker.CoreBusiness.Models;
using TermTracker.UseCases.Interfaces;
using TermTracker.UseCases.PluginInterfaces;

namespace TermTracker.UseCases.CourseUseCases;
public class ViewCoursesUseCase : IViewCoursesUseCase
{
    private readonly ICourseRepository courseRepository;

    public ViewCoursesUseCase(ICourseRepository courseRepository)
    {
        this.courseRepository = courseRepository;
    }
    public async Task<List<Course>> ExecuteAsync(int termId)
    {
        return await courseRepository.GetCoursesAsync(termId);
    }
}
