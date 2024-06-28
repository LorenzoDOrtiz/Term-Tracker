using System.Collections.ObjectModel;
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
            new Course
            {
                TermId = 1,
                Id = 1,
                Name = "Introduction to Computer Science",
                StartDate = DateTime.Now.AddMinutes(2),
                EndDate = DateTime.Now.AddMinutes(3),
                Status = "In Progress",
                Notes = "This is a foundational course for computer science students.",
                Description = "This course is designed to provide students with a foundational understanding of computer science principles and practices. Throughout the course, students will explore the basics of programming, algorithms, data structures, computer systems, and software development.",
                Instructor = new Instructor
                {
                    Name = "Dr. John Doe",
                    PhoneNumber = "12345678",
                    Email = "JohnDoe@gmail.com"
                },
                Assessments = new ObservableCollection<Assessment>
                {
                    new ObjectiveAssessment
                    {
                        Name = "Objective Assessment",
                        StartDate = DateTime.Now.AddDays(7),
                        EndDate = DateTime.Now.AddDays(8),
                    },
                    new PerformanceAssessment
                    {
                        Name = "Performance Assessment",
                        StartDate = DateTime.Now.AddDays(7),
                        EndDate = DateTime.Now.AddDays(8),
                    },
                },
            }
        };
    }

    public Task<List<Course>> GetAllAsync(int termId)
    {
        var filteredCourses = _courses.Where(course => course.TermId == termId).ToList();
        return Task.FromResult(filteredCourses);
    }

    public Task AddAsync(Course course)
    {
        var maxId = _courses.Max(x => x.Id);
        course.Id = maxId + 1;
        _courses.Add(course);

        return Task.CompletedTask;
    }

    public Task<Course> GetByIdAsync(int courseId)
    {
        var course = _courses.FirstOrDefault(x => x.Id == courseId);
        if (course != null)
        {
            return Task.FromResult(new Course
            {
                Id = courseId,
                TermId = course.TermId,
                Name = course.Name,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                Status = course.Status,
                Notes = course.Notes,
                Description = course.Description,
                Instructor = course.Instructor,
                Assessments = course.Assessments,
                StartDateAlerts = course.StartDateAlerts,
                EndDateAlerts = course.EndDateAlerts
            });
        }

        return Task.FromResult<Course>(null);
    }

    public Task UpdateAsync(int courseId, Course course)
    {
        if (courseId != course.Id) return Task.CompletedTask;

        var courseToUpdate = _courses.FirstOrDefault(x => x.Id == courseId);
        if (courseToUpdate != null)
        {
            courseToUpdate.Id = courseId;
            courseToUpdate.TermId = course.TermId;
            courseToUpdate.Name = course.Name;
            courseToUpdate.StartDate = course.StartDate;
            courseToUpdate.EndDate = course.EndDate;
            courseToUpdate.Status = course.Status;
            courseToUpdate.Notes = course.Notes;
            courseToUpdate.Description = course.Description;
            courseToUpdate.Instructor = course.Instructor;
            courseToUpdate.Assessments = course.Assessments;
            courseToUpdate.StartDateAlerts = course.StartDateAlerts;
            courseToUpdate.EndDateAlerts = course.EndDateAlerts;
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int courseId)
    {
        var course = _courses.FirstOrDefault(x => x.Id == courseId);
        if (course != null)
        {
            _courses.Remove(course);
        }

        return Task.CompletedTask;
    }
}
