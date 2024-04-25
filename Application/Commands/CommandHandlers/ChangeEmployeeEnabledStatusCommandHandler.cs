using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Application.Commands.CommandHandlers
{
    public class ChangeEmployeeEnabledStatusCommandHandler : IRequestHandler<ChangeEmployeeEnabledStatusCommand>
    {
        private readonly IConfiguration _configuration;
        public ChangeEmployeeEnabledStatusCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=taskdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        public async Task Handle(ChangeEmployeeEnabledStatusCommand request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration["ConnectionString"]))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Employee SET Enable = @Enable WHERE ID = @Id", connection);
                command.Parameters.AddWithValue("@Enable", request.EnbaledStatus);
                command.Parameters.AddWithValue("@ID", request.Id);

                cancellationToken.ThrowIfCancellationRequested();

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected != 1)
                {
                    throw new KeyNotFoundException($"Employee with id {request.Id} is not found in the database");
                }
            }
        }
    }
}