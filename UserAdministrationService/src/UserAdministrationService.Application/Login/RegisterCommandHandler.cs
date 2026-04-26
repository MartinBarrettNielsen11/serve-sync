using System.Runtime.CompilerServices;
using SharedKernel.Results;

namespace UserAdministrationService.Application.Login;

internal sealed class RegisterCommandHandler
{
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<Result>))]
#pragma warning disable CA1822
    internal async ValueTask<Result> Handle(RegisterCommand command, CancellationToken cancellationToken)
#pragma warning restore CA1822
    {
        // check of user exists
        // create new instance of User entity
        // return some authentication dto including a token
    }
}