using TermTracker.CoreBusiness.Models;
using TermTracker.UseCases.PluginInterfaces;
using TermTracker.UseCases.UseCaseInterfaces;

namespace TermTracker.UseCases.UseCases
{
    public class ViewUseCase<T> : IViewUseCase<T> where T : class
    {
        private readonly ICourseRepository<Course> courseRepository;
        private readonly ITermRepository<Term> termRepository;

        public ViewUseCase(ICourseRepository<Course> courseRepository, ITermRepository<Term> termRepository)
        {
            this.courseRepository = courseRepository;
            this.termRepository = termRepository;
        }

        public async Task<T> ExecuteAsync(int id)
        {
            if (typeof(T) == typeof(Course))
            {
                return await courseRepository.GetByIdAsync(id) as T;
            }
            else if (typeof(T) == typeof(Term))
            {
                return await termRepository.GetByIdAsync(id) as T;
            }
            else
            {
                throw new ArgumentException($"Type {typeof(T).Name} is not supported.");
            }
        }
    }
}
