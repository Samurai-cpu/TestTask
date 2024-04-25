using Core.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Application.Queries.QueryHandlers
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeDTO>
    {
        private readonly IConfiguration _configuration;
        public GetEmployeeByIdQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<GetEmployeeDTO> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration["ConnectionString"]))
            {
                connection.Open();
                var sqlString = """
                    SELECT *
                    FROM Employee AS Manager
                    WHERE Manager.ID = @Id
                    UNION 
                    SELECT *
                    FROM Employee AS Subordinates
                    WHERE Subordinates.ManagerID = @Id
                    """;
                var command = new SqlCommand(sqlString, connection);
                command.Parameters.AddWithValue("@Id", request.Id);

                cancellationToken.ThrowIfCancellationRequested();

                var reader = await command.ExecuteReaderAsync();

                return HandleDBResponse(reader, request.Id);
            }
        }

        private static GetEmployeeDTO HandleDBResponse(SqlDataReader reader, int id)
        {
            var manager = new GetEmployeeDTO();
            var subordinates = new List<Employee>();
            if (!reader.HasRows)
            {
                throw new KeyNotFoundException($"Employee with id {id} is not found in the database");
            }
            while (reader.Read())
            {
                var employee = new GetEmployeeDTO
                {
                    Id = (int)reader["ID"],
                    Name = (string)reader["Name"],
                    Enable = (bool)reader["Enable"],
                    ManagerId = Convert.IsDBNull(reader["ManagerID"]) ? null : (int?)reader["ManagerID"],
                };

                if (employee.Id == id)
                {
                    manager = employee;
                }
                else
                {
                    subordinates.Add(employee);
                }
            }
            manager.Subordinates = subordinates;
            return manager;
        }
    }
}