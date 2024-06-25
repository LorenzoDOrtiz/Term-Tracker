using TermTracker.CoreBusiness.Models;
using TermTracker.UseCases.PluginInterfaces;
using TermTracker.UseCases.UseCaseInterfaces;

namespace TermTracker.UseCases.UseCases;

public class ViewCollectionUseCase<T> : IViewCollectionUseCase<T> where T : class
{
    private readonly ICourseRepository<Course> courseRepository;
    private readonly ITermRepository<Term> termRepository;

    public ViewCollectionUseCase(ICourseRepository<Course> courseRepository, ITermRepository<Term> termRepository)
    {
        this.courseRepository = courseRepository;
        this.termRepository = termRepository;
    }

    public async Task<List<T>> ExecuteAsync(int termId)
    {
        if (typeof(T) == typeof(Course))
        {
            var courses = await courseRepository.GetAllAsync(termId);
            return courses as List<T>; // Safe cast assuming courseRepository returns List<Course>
        }
        return null;
    }

    public async Task<List<T>> ExecuteAsync()
    {
        if (typeof(T) == typeof(Term))
        {
            var terms = await termRepository.GetAllAsync();
            return terms as List<T>; // Safe cast assuming termRepository returns List<Term>
        }
        return null;
    }
}
