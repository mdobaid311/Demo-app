using System;

namespace DemoApp.Contracts.Users
{
    public record LoginUserRequest(
        string Email,
        string Password
    );
}
