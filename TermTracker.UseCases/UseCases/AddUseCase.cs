using TermTracker.CoreBusiness.Models;
using TermTracker.UseCases.PluginInterfaces;
using TermTracker.UseCases.UseCaseInterfaces;

namespace TermTracker.UseCases.UseCaseImplementations;

public class AddUseCase<T> : IAddUseCase<T> where T : class
{
    private readonly ICourseRepository<Course> courseRepository;
    private readonly ITermRepository<Term> termRepository;

    public AddUseCase(ICourseRepository<Course> courseRepository, ITermRepository<Term> termRepository)
    {
        this.courseRepository = courseRepository;
        this.termRepository = termRepository;
    }

    public async Task ExecuteAsync(T obj)
    {
        if (obj is Course)
            await courseRepository.AddAsync(obj as Course);
        else if (obj is Term)
            await termRepository.AddAsync(obj as Term);
    }
}
