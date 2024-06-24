using TermTracker.CoreBusiness.Models;
using TermTracker.UseCases.Interfaces;
using TermTracker.UseCases.PluginInterfaces;

namespace TermTracker.UseCases.CourseUseCases;
public class AddCourseUseCase : IAddCourseUseCase
{
    private readonly ICourseRepository courseRepository;

    public AddCourseUseCase(ICourseRepository courseRepository)
    {
        this.courseRepository = courseRepository;
    }

    public async Task ExecuteAsync(Course course)
    {
        await courseRepository.AddCourseAsync(course);
    }
}
