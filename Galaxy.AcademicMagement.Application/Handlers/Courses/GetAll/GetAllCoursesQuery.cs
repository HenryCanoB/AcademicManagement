using Galaxy.AcademicMagement.Application.Dto.Courses;
using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Courses.GetAll
{
    public record GetAllCoursesQuery() : IRequest<ICollection<CourseResponse>>;
}
