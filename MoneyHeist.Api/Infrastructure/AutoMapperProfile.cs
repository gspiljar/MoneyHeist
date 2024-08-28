using AutoMapper;
using MoneyHeist.Api.ApiLogic.Members;
using MoneyHeist.Api.ApiLogic.Members.Dtos;
using MoneyHeist.Api.ApiLogic.Members.Models;

namespace MoneyHeist.Api.Infrastructure
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<AddMemberHandler.Command, MemberModel>();
            CreateMap<SkillDto, SkillModel>();
        }
	}
}
