using Dapper;
using Demkin.Infrastructure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Demkin.Listen.WebApi.Admin.Application.Queries
{
    public class GetCategoryListQuery : IRequest<List<CategoryDto>>
    {
        public string? Title { get; set; }
    }

    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, List<CategoryDto>>
    {
        private readonly string _connectionString = string.Empty;

        public GetCategoryListQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("DbConnection:MasterDb_Listen") ?? throw new Exception("connection为null");
        }

        public async Task<List<CategoryDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            string sql = @"SELECT tc.Id,tc.Title,tc.CoverUrl,tc.SequenceNumber,tc.CreateTime
                                    FROM Category tc
                                    WHERE tc.Title like @title";
            var parmeter = new
            {
                title = string.Format("{0}%", request.Title)
            };

            var debugStr = DapperSqlHelper.GetSqlStr(sql, parmeter);

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var result = await connection.QueryAsync<CategoryDto>(sql, parmeter);

            return result.ToList();
        }
    }
}