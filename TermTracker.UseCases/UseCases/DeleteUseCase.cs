using TermTracker.CoreBusiness.Models;
using TermTracker.UseCases.PluginInterfaces;
using TermTracker.UseCases.UseCaseInterfaces;

namespace TermTracker.UseCases.UseCaseImplementations;

public class DeleteUseCase<T> : IDeleteUseCase<T> where T : class
{
    private readonly ICourseRepository<Course> courseRepository;
    private readonly ITermRepository<Term> termRepository;

    public DeleteUseCase(ICourseRepository<Course> courseRepository, ITermRepository<Term> termRepository)
    {
        this.courseRepository = courseRepository;
        this.termRepository = termRepository;
    }

    public async Task ExecuteAsync(int id)
    {
        if (typeof(T) == typeof(Course))
            await courseRepository.DeleteAsync(id);
        else if (typeof(T) == typeof(Term))
            await termRepository.DeleteAsync(id);
        // Handle other types if needed
    }
}
