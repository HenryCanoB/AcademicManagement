using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Application.Dto.Courses;
using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Courses.GetAll
{
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, ICollection<CourseResponse>>
    {
        private readonly ICourseRepository _courseRepository;

        public GetAllCoursesQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<ICollection<CourseResponse>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseRepository.GetAvailableCoursesAsync();
            var response = courses.Select(c => new CourseResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Credits = c.Credits,
                ProfessorId = c.ProfessorId,
                ProfessorName = c.Professor != null ? c.Professor.GetFullName() : string.Empty,
                IsActive = c.IsActive
            }).ToList();

            return response;
        }
    }
}
