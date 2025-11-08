using Galaxy.AcademicMagement.Domain.Enums;

namespace Galaxy.AcademicMagement.Domain.Entities
{
    public class Enrollment : BaseEntity
    {
        public Guid StudentId { get; private set; }
        public Guid CourseId { get; private set; }
        public DateTime EnrollmentDate { get; private set; }
        public EnrollmentCondition Condition { get; private set; }
        public Student Student { get; private set; }
        public Course Course { get; private set; }

        protected Enrollment() { }

        public Enrollment(Guid studentId, Guid courseId, DateTime? enrollmentDate = null)
        {
            if (studentId == Guid.Empty)
                throw new ArgumentException("Student ID cannot be empty", nameof(studentId));

            if (courseId == Guid.Empty)
                throw new ArgumentException("Course ID cannot be empty", nameof(courseId));

            StudentId = studentId;
            CourseId = courseId;
            EnrollmentDate = enrollmentDate ?? DateTime.UtcNow;
            Condition = EnrollmentCondition.Enrolled;
        }


        public void Withdraw()
        {
            Condition = EnrollmentCondition.Withdrawn;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Reenroll()
        {
            if (Condition == EnrollmentCondition.Withdrawn)
            {
                Condition = EnrollmentCondition.Enrolled;
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public bool IsEnrolled => Condition == EnrollmentCondition.Enrolled;
    }
}
