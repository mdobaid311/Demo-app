using Dapper;
using DemoApp.Application.Common.Interfaces;
using DemoApp.Application.Common.Interfaces.Repositories;
using DemoApp.Domain.Employees;
using FluentEmail.Core;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;
using System.Text;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public EmployeeRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    /// <summary>
    /// Adds a new employee to the database using a stored procedure.
    /// </summary>
    public async Task AddAsync(Employee employee)
    {
        const string storedProcedure = "sp_Employee_CreateEmployee";

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(storedProcedure, new
            {
                EmployeeID = employee.EmployeeID,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                IsDeleted = false
            }, commandType: CommandType.StoredProcedure);
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Exception: {ex.Message}");
            throw new Exception("An error occurred while adding the employee to the database.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw new Exception("An unexpected error occurred while adding the employee.", ex);
        }
    }

    /// <summary>
    /// Deletes a employee from the database using a stored procedure.
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        const string storedProcedure = "sp_Employee_DeleteEmployee";

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(storedProcedure, new { EmployeeID = id }, commandType: CommandType.StoredProcedure);
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Exception: {ex.Message}");
            throw new Exception("An error occurred while deleting the employee from the database.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw new Exception("An unexpected error occurred while deleting the employee.", ex);
        }
    }

    /// <summary>
    /// Checks if a employee exists in the database based on their email.
    /// </summary>
    public async Task<bool> ExistsAsync(string email)
    {
        const string sql = "SELECT COUNT(1) FROM Employee WHERE Email = @Email";

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            int count = await connection.ExecuteScalarAsync<int>(sql, new { Email = email });
            return count > 0;
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Exception: {ex.Message}");
            throw new Exception("An error occurred while checking if the employee exists in the database.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw new Exception("An unexpected error occurred while checking if the employee exists.", ex);
        }
    }

    /// <summary>
    /// Retrieves all employees from the database using a stored procedure.
    /// </summary>
    public async Task<List<Employee>> GetAllAsync()
    {
        const string storedProcedure = "sp_Employee_GetEmployees";

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            var employees = await connection.QueryAsync<Employee>(storedProcedure, commandType: CommandType.StoredProcedure);
            return employees.ToList();
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Exception: {ex.Message}");
            throw new Exception("An error occurred while retrieving all employees from the database.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw new Exception("An unexpected error occurred while retrieving all employees.", ex);
        }
    }

    /// <summary>
    /// Retrieves a single employee by their ID using a stored procedure.
    /// </summary>
    public async Task<Employee> GetAsync(Guid id)
    {
        const string sql = "SELECT [EmployeeID],[FirstName],[LastName],[PhoneNumber],[Email] FROM Employee WHERE EmployeeID = @EmployeeID";

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            // Use QueryFirstOrDefaultAsync to map the entire row to the Employee object
            Employee employee = await connection.QueryFirstOrDefaultAsync<Employee>(sql, new { EmployeeID = id });

            if (employee != null)
            {
                return employee;
            }
            else
            {
                throw new Exception("Employee does not exist.");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Exception: {ex.Message}");
            throw new Exception("An error occurred while retrieving the employee from the database.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw new Exception("An unexpected error occurred while retrieving the employee.", ex);
        }
    }


    /// <summary>
    /// Retrieves a single employee by their email using a stored procedure.
    /// </summary>
    public async Task<Employee> GetByEmailAsync(string email)
    {
        const string storedProcedure = "sp_Employee_GetByEmail"; // Assuming it also supports filtering by EmployeeID

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            var employee = await connection.QueryFirstOrDefaultAsync<Employee>(
                storedProcedure,
                new { Email = email },
                commandType: CommandType.StoredProcedure);

            if (employee == null)
            {
                throw new Exception($"Employee with Email: {email} not found.");
            }

            return employee;
        }
        catch (SqlException ex)
        {
            throw new Exception("An error occurred while retrieving the employee from the database.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw new Exception("An unexpected error occurred while retrieving the employee.", ex);
        }
    }

    /// <summary>
    /// Updates an existing employee in the database using a stored procedure.
    /// </summary>
    public async Task UpdateAsync(Employee employee)
    {
        const string storedProcedure = "sp_Employee_UpdateEmployee";

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(storedProcedure, new
            {
                EmployeeID = employee.EmployeeID, 
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email
            }, commandType: CommandType.StoredProcedure);
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Exception: {ex.Message}");
            throw new Exception("An error occurred while updating the employee in the database.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw new Exception("An unexpected error occurred while updating the employee.", ex);
        }
    }
}
