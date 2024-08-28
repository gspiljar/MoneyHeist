using AutoMapper.Execution;
using MoneyHeist.Api.ApiLogic.Members.Models;

namespace MoneyHeist.Api.ApiLogic.Members.Interfaces
{
    public interface IMemberRepository
    {
        Task<int?> AddMemberAsync(MemberModel member);
        Task<MemberModel?> GetMemberByEmailAsync(string email);
    }
}
