using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DemoApp.Application.Common.Interfaces;
using DemoApp.Application.Common.Interfaces.Repositories;
using DemoApp.Domain.Salaries;
using Microsoft.Data.SqlClient;

namespace DemoApp.Infrastructure.Salaries
{
    public class SalaryRepository : ISalaryRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public SalaryRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> SalaryExist(Guid salaryID)
        {
            const string query = "SELECT COUNT(1) FROM Salaries WHERE SalaryID = @SalaryID";

            try
            {
                using var connection = _connectionFactory.CreateConnection();
                var result = await connection.ExecuteScalarAsync<int>(
                    query,
                    new { SalaryID = salaryID }
                );

                return result > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Exception: {ex.Message}");
                throw new Exception("An error occurred while checking the salary record.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw new Exception(
                    "An unexpected error occurred while checking the salary record.",
                    ex
                );
            }
        }

        /// <summary>
        /// Adds a new salary record to the database using a stored procedure.
        /// </summary>
        public async Task<Salary> CreateSalaryAsync(Salary salary)
        {
            const string storedProcedure = "spDB_Salary_Insert";

            try
            {
                using var connection = _connectionFactory.CreateConnection();
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeID", salary.EmployeeID, DbType.Guid);
                parameters.Add("@SalaryAmount", salary.SalaryAmount, DbType.Decimal);
                parameters.Add("@Month", salary.Month, DbType.String);
                parameters.Add(
                    "@SalaryID",
                    dbType: DbType.Guid,
                    direction: ParameterDirection.Output
                );

                await connection.ExecuteAsync(
                    storedProcedure,
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                salary.SalaryID = parameters.Get<Guid>("@SalaryID");
                return salary;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Exception: {ex.Message}");
                throw new Exception("An error occurred while adding the salary record.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw new Exception(
                    "An unexpected error occurred while adding the salary record.",
                    ex
                );
            }
        }

        /// <summary>
        /// Deletes a salary record from the database using a stored procedure.
        /// </summary>
        public async Task DeleteSalaryAsync(Guid salaryID)
        {
            const string storedProcedure = "spDB_Salary_Delete";

            try
            {
                using var connection = _connectionFactory.CreateConnection();
                await connection.ExecuteAsync(
                    storedProcedure,
                    new { SalaryID = salaryID },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Exception: {ex.Message}");
                throw new Exception("An error occurred while deleting the salary record.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw new Exception(
                    "An unexpected error occurred while deleting the salary record.",
                    ex
                );
            }
        }

        /// <summary>
        /// Retrieves a salary record by Employee ID using a stored procedure.
        /// </summary>
        public async Task<List<Salary>> GetSalaryByEmployeeIDAsync(Guid employeeID)
        {
            const string storedProcedure = "spDB_Salary_GetByEmployeeID";

            try
            {
                using var connection = _connectionFactory.CreateConnection();
                var salaries = await connection.QueryAsync<Salary>(
                    storedProcedure,
                    new { EmployeeID = employeeID },
                    commandType: CommandType.StoredProcedure
                );

                return salaries.AsList(); // Ensures returning List<T>
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Exception: {ex.Message}");
                throw; // Preserve stack trace
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // Preserve stack trace
            }
        }

    }
}
