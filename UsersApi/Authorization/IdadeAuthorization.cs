using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UsersApi.Authorization;

public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
    {
        var dataNascimentoClaim = context.User.FindFirst(Claim => Claim.Type == ClaimTypes.DateOfBirth);

        if (dataNascimentoClaim is null)
        {
            return Task.CompletedTask;
        }

        var dataNascimento = Convert.ToDateTime(dataNascimentoClaim.Value);

        var idadeUser = DateTime.Today.Year - dataNascimento.Year;

        if (dataNascimento > DateTime.Today.AddYears(-idadeUser)) idadeUser--;

        if (idadeUser >= requirement.Idade) context.Succeed(requirement);

        return Task.CompletedTask;
         
    }
}
