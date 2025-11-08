using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Application.Contracts.Services;
using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Enrollments.Withdraw
{
    public class WithdrawEnrollmentCommandHandler : IRequestHandler<WithdrawEnrollmentCommand, bool>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IAcademicManagementUnitOfWork _unitOfWork;

        public WithdrawEnrollmentCommandHandler(
            IEnrollmentRepository enrollmentRepository,
            IAcademicManagementUnitOfWork unitOfWork)
        {
            _enrollmentRepository = enrollmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(WithdrawEnrollmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var enrollment = await _enrollmentRepository.GetByIdAsync(request.EnrollmentId);

                if (enrollment == null)
                {
                    throw new ApplicationException($"Enrollment with ID {request.EnrollmentId} not found.");
                }

                enrollment.Withdraw();

                _enrollmentRepository.Update(enrollment);

                await _unitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
