namespace TermTracker.Plugins.DataStore.InMemory;
public class CourseInMemoryRepository : ICourseRepository<Course>
{
    public List<Course> _courses;
    public CourseInMemoryRepository()
    {
        _courses = new()
        {
            new()
            {
                TermId = 1,
                Id = 1,
                Name = "Mobile Application Development Using C# - C971",
                StartDate = DateTime.Now.AddMinutes(2),
                EndDate = DateTime.Now.AddMinutes(3),
                Status = "Plan to Take",
                Notes = "Study the .NET MAUI microsoft learning materials to prepare for this class.",
                Description = "Mobile Application Development Using C# introduces students to programming for mobile devices. Building on students’ previous programming knowledge in C#, this course explores a broad range of topics, including mobile user interface design and development; building applications that adapt to different mobile devices and platforms; managing data using a local database; and consuming REST-based web services. In this course, students will focus on developing skills using the latest framework designed to provide a more modern and streamlined development experience. This framework will help students design and code cross-platform applications that work on a range of mobile devices. There are several prerequisites for this course: Software I and II, and UI Design.",
                Instructor = new Instructor
                {
                    Name = "Anika Patel",
                    PhoneNumber = "555-123-4567",
                    Email = "anika.patel@strimeuniversity.edu"
                },
                Assessments = new()
                {
                    new Assessment
                    {
                        Id = 1,
                        CourseId = 1,
                        Type = "Objective Assessment",
                        StartDate = DateTime.Now.AddMinutes(2),
                        EndDate = DateTime.Now.AddMinutes(3)
                    },
                    new Assessment
                    {
                        Id = 2,
                        CourseId = 1,
                        Type = "Performance Assessment",
                        StartDate = DateTime.Now.AddMinutes(2),
                        EndDate = DateTime.Now.AddMinutes(3)
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

        return Task.FromResult(new Course());
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
