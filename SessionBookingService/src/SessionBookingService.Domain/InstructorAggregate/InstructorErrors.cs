using SharedKernel.Results;

namespace SessionBookingService.Domain.InstructorAggregate;

internal static class InstructorErrors
{
	internal static readonly Error SessionCannotOverlap = Error.Failure(
		"Instructor.CannotHaveMultipleOverlappingSessions",
		"An instructor cannot have two or more overlapping sessions");
}