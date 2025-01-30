using DemoApp.Application.Common.Security.Request;

using ErrorOr;

namespace DemoApp.Application.Common.Interfaces;

public interface IAuthorizationService
{
    ErrorOr<Success> AuthorizeCurrentUser<T>(
        IAuthorizableRequest<T> request,
        List<string> requiredRoles,
        List<string> requiredPermissions,
        List<string> requiredPolicies);
}