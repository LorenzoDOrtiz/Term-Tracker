using TermTracker.CoreBusiness.Models;
using TermTracker.UseCases.PluginInterfaces;
using TermTracker.UseCases.UseCaseInterfaces;

namespace TermTracker.UseCases.UseCases;

public class EditUseCase<T> : IEditUseCase<T> where T : class
{
    private readonly ICourseRepository<Course> courseRepository;
    private readonly ITermRepository<Term> termRepository;

    public EditUseCase(ICourseRepository<Course> courseRepository, ITermRepository<Term> termRepository)
    {
        this.courseRepository = courseRepository;
        this.termRepository = termRepository;
    }

    public async Task ExecuteAsync(int id, T obj)
    {
        if (typeof(T) == typeof(Course))
            await courseRepository.UpdateAsync(id, obj as Course);
        else if (typeof(T) == typeof(Term))
            await termRepository.UpdateAsync(id, obj as Term);
        // Handle other types if needed
    }
}
