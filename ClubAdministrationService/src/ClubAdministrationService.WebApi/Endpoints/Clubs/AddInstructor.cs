using ClubAdministrationService.Application.Clubs.AddInstructor;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.WebApi.Infrastructure;
using MediatR;
using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Endpoints.Clubs;

public sealed class AddInstructor : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(pattern: "subscriptions/{subscriptionId:guid}/clubs/{clubId:guid}/instructors",
                    handler: async (AddInstructorRequest request,
                                    Guid subscriptionId,
                                    Guid clubId,
                                    ISender sender,
                                    CancellationToken cancellationToken) =>
                {
                    AddInstructorCommand command = new(subscriptionId, clubId, request.InstructorId);

                    Result addTrainerResult = await sender.Send(command, cancellationToken);

                    IResult result = addTrainerResult.Match(
                        onSuccess: () => Results.Ok(clubId),
                        onFailure: err => ProblemDetailsMapper.Problem(errors: [err.Error]));

                    return result;
                })
            .WithTags(Tags.Clubs)
            .WithSummary("Add instructor")
            .WithDescription("Add instructor for a subscription (and a club)");
        //.RequireAuthorization();
    }
}

