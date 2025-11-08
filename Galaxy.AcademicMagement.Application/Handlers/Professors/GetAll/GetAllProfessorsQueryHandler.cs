using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Application.Dto.Professors;
using Mapster;
using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Professors.GetAll
{
    public class GetAllProfessorsQueryHandler : IRequestHandler<GetAllProfessorsQuery, ICollection<ProfessorResponse>>
    {
        private readonly IProfessorRepository _professorRepository;

        public GetAllProfessorsQueryHandler(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public async Task<ICollection<ProfessorResponse>> Handle(GetAllProfessorsQuery request, CancellationToken cancellationToken)
        {
            var professors = await _professorRepository.ListAsync();

            return professors.Adapt<ICollection<ProfessorResponse>>();

        }
    }
}
