using Dapper;
using System.Data;
using System.Data.SqlClient;

using MoneyHeist.Api.ApiLogic.Members.Interfaces;
using MoneyHeist.Api.ApiLogic.Members.Models;
using MoneyHeist.Api.AppLogic;
using MoneyHeist.Api.Infrastructure.Interfaces;

namespace MoneyHeist.Api.ApiLogic.Members
{
    public class MemberRepository : RepositoryBase, IMemberRepository
    {
        private readonly string? _connectionString;

        public MemberRepository(IDatabaseContext databaseContext)
        {
            _connectionString = databaseContext?.GetDefaultConnectionString() ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        public async Task<int?> AddMemberAsync(MemberModel member)
        {
            var dt = ConvertGenericToDataTable<SkillModel>(member.Skills);

            if (dt == null)
            {
                return null;
            }

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", null, DbType.Int32, ParameterDirection.Output);
            parameters.Add("Name", member.Name);
            parameters.Add("Sex", member.Sex);
            parameters.Add("Email", member.Email);
            parameters.Add("MainSkill", member.MainSkill);
            parameters.Add("Status", member.Status);
            parameters.Add("Skills", dt.AsTableValuedParameter("SkillInsertType"));

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var rowsAffected = await connection.ExecuteAsync("dbo.spMemberInsert", parameters, commandType: CommandType.StoredProcedure);

            if (rowsAffected == 0)
            {
                return null;
            }

            return parameters.Get<int?>("@Id");
        }

        public async Task<MemberModel?> GetMemberByEmailAsync(string email)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var member = await connection.QuerySingleOrDefaultAsync<MemberModel>(
                "dbo.spGetMemberByEmail",
                new { Email = email },
                commandType: CommandType.StoredProcedure);

            return member;
        }
    }
}
