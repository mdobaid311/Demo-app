using Dapper;
using DemoApp.Application.Common.Interfaces;
using DemoApp.Application.Common.Interfaces.Repositories;
using DemoApp.Domain.Users;
using FluentEmail.Core;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;
using System.Text;

public class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UserRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    /// <summary>
    /// Adds a new user to the database using a stored procedure.
    /// </summary>
    public async Task AddAsync(User user)
    {
        const string storedProcedure = "sp_User_CreateUser";

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(storedProcedure, new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                IsDeleted = false, // Default value for a new user
                RoleID = user.RoleID,
            }, commandType: CommandType.StoredProcedure);
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Exception: {ex.Message}");
            throw new Exception("An error occurred while adding the user to the database.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw new Exception("An unexpected error occurred while adding the user.", ex);
        }
    }

    /// <summary>
    /// Deletes a user from the database using a stored procedure.
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        const string storedProcedure = "sp_User_DeleteUser";

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(storedProcedure, new { UserID = id }, commandType: CommandType.StoredProcedure);
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Exception: {ex.Message}");
            throw new Exception("An error occurred while deleting the user from the database.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw new Exception("An unexpected error occurred while deleting the user.", ex);
        }
    }

    /// <summary>
    /// Checks if a user exists in the database based on their email.
    /// </summary>
    public async Task<bool> ExistsAsync(string email)
    {
        const string sql = "SELECT COUNT(1) FROM Users WHERE Email = @Email";

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            int count = await connection.ExecuteScalarAsync<int>(sql, new { Email = email });
            return count > 0;
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Exception: {ex.Message}");
            throw new Exception("An error occurred while checking if the user exists in the database.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw new Exception("An unexpected error occurred while checking if the user exists.", ex);
        }
    }

    /// <summary>
    /// Retrieves all users from the database using a stored procedure.
    /// </summary>
    public async Task<List<User>> GetAllAsync()
    {
        const string storedProcedure = "sp_User_GetUsers";

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            var users = await connection.QueryAsync<User>(storedProcedure, commandType: CommandType.StoredProcedure);
            return users.ToList();
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Exception: {ex.Message}");
            throw new Exception("An error occurred while retrieving all users from the database.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw new Exception("An unexpected error occurred while retrieving all users.", ex);
        }
    }

    /// <summary>
    /// Retrieves a single user by their ID using a stored procedure.
    /// </summary>
    public async Task<User> GetAsync(Guid id)
    {
        const string sql = "SELECT [UserID],[FirstName],[LastName],[PhoneNumber],[Email] FROM Users WHERE UserID = @UserID";

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            // Use QueryFirstOrDefaultAsync to map the entire row to the User object
            User user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { UserID = id });

            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("User does not exist.");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Exception: {ex.Message}");
            throw new Exception("An error occurred while retrieving the user from the database.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw new Exception("An unexpected error occurred while retrieving the user.", ex);
        }
    }


    /// <summary>
    /// Retrieves a single user by their email using a stored procedure.
    /// </summary>
    public async Task<User> GetByEmailAsync(string email)
    {
        const string storedProcedure = "sp_User_GetByEmail"; // Assuming it also supports filtering by UserID

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            var user = await connection.QueryFirstOrDefaultAsync<User>(
                storedProcedure,
                new { Email = email },
                commandType: CommandType.StoredProcedure);

            if (user == null)
            {
                throw new Exception($"User with Email: {email} not found.");
            }

            return user;
        }
        catch (SqlException ex)
        {
            throw new Exception("An error occurred while retrieving the user from the database.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw new Exception("An unexpected error occurred while retrieving the user.", ex);
        }
    }

    /// <summary>
    /// Updates an existing user in the database using a stored procedure.
    /// </summary>
    public async Task UpdateAsync(User user)
    {
        const string storedProcedure = "sp_User_EditUser";

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(storedProcedure, new
            {
                UserID = user.UserID, 
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
            }, commandType: CommandType.StoredProcedure);
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Exception: {ex.Message}");
            throw new Exception("An error occurred while updating the user in the database.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw new Exception("An unexpected error occurred while updating the user.", ex);
        }
    }
}
