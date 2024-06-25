using TermTracker.CoreBusiness.Models;
using TermTracker.UseCases.PluginInterfaces;

namespace TermTracker.Plugins.DataStore.InMemory;
public class CourseInMemoryRepository : ICourseRepository<Course>
{
    public List<Course> _courses;
    public CourseInMemoryRepository()
    {
        _courses = new List<Course>()
        {
            new Course {TermId = 1, CourseId = 1, CourseName = "Introduction to Computer Science", CourseStartDate = DateTime.Now, CourseEndDate = DateTime.Now.AddMonths(1)},
            new Course {TermId = 1, CourseId = 2, CourseName = "Data Structures and Algorithms", CourseStartDate = DateTime.Now.AddMonths(1), CourseEndDate = DateTime.Now.AddMonths(2)},
            new Course {TermId = 1, CourseId = 3, CourseName = "Object-Oriented Programming in C#", CourseStartDate = DateTime.Now.AddMonths(2), CourseEndDate = DateTime.Now.AddMonths(3)},
            new Course {TermId = 1, CourseId = 4, CourseName = "Web Development with ASP.NET Core", CourseStartDate = DateTime.Now.AddMonths(3), CourseEndDate = DateTime.Now.AddMonths(4)},
            new Course {TermId = 1, CourseId = 5, CourseName = "Database Design and SQL Fundamentals", CourseStartDate = DateTime.Now.AddMonths(4), CourseEndDate = DateTime.Now.AddMonths(5)},
            new Course {TermId = 2, CourseId = 6, CourseName = "Advanced Object-Oriented Programming in C#", CourseStartDate = DateTime.Now.AddMonths(5), CourseEndDate = DateTime.Now.AddMonths(6)}

        };

    }

    public Task<List<Course>> GetAllAsync(int termId)
    {
        var filteredCourses = _courses.Where(course => course.TermId == termId).ToList();
        return Task.FromResult(filteredCourses);
    }

    public Task AddAsync(Course course)
    {
        var maxId = _courses.Max(x => x.CourseId);
        course.CourseId = maxId + 1;
        _courses.Add(course);

        return Task.CompletedTask;
    }
    public Task<Course> GetByIdAsync(int courseId)
    {
        var course = _courses.FirstOrDefault(x => x.CourseId == courseId);
        if (course != null)
        {
            return Task.FromResult(new Course
            {
                CourseId = courseId,
                TermId = course.TermId,
                CourseName = course.CourseName,
                CourseStartDate = course.CourseStartDate,
                CourseEndDate = course.CourseEndDate
            });
        }
        return Task.FromResult<Course>(null);
    }

    public Task UpdateAsync(int courseId, Course course)
    {
        if (courseId != course.CourseId) return Task.CompletedTask;

        var courseToUpdate = _courses.FirstOrDefault(x => x.CourseId == courseId);
        if (courseToUpdate != null)
        {
            courseToUpdate.CourseId = courseId;
            courseToUpdate.TermId = course.TermId;
            courseToUpdate.CourseName = course.CourseName;
            courseToUpdate.CourseStartDate = course.CourseStartDate;
            courseToUpdate.CourseEndDate = course.CourseEndDate;
        }

        return Task.CompletedTask;
    }
    public Task DeleteAsync(int courseId)
    {
        var course = _courses.FirstOrDefault(x => x.CourseId == courseId);
        if (course != null)
        {
            _courses.Remove(course);
        }

        return Task.CompletedTask;
    }
}
