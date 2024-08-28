using AutoMapper;
using FluentValidation;
using LanguageExt.Common;
using MediatR;
using Microsoft.VisualBasic;
using MoneyHeist.Api.ApiLogic.Members.Dtos;
using MoneyHeist.Api.ApiLogic.Members.Interfaces;
using MoneyHeist.Api.ApiLogic.Members.Models;

namespace MoneyHeist.Api.ApiLogic.Members
{
    public class AddMemberHandler
    {
        public class Command : IRequest<Result<CommandResponse>>
        {
            public string? Name { get; set; }
            public string? Sex { get; set; }
            public string? Email { get; set; }
            public List<SkillDto>? Skills { get; set; }
            public string? MainSkill { get; set; }
            public string? Status { get; set; }
        }

        public class CommandResponse
        {
            public int? MemberId { get; set; }
            public bool IsSuccess { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Sex).NotEmpty().Must(x => x == "M" || x == "F");
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Skills).NotEmpty();
                RuleForEach(x => x.Skills).ChildRules(skills =>
                {
                    skills.RuleFor(skill => skill.Name).NotEmpty();
                    skills.RuleFor(skill => skill.Level)
                          .Matches(@"^\*{1,10}$")
                          .WithMessage("Skill level must be between 1 and 10 asterisks.");
                });
                RuleFor(x => x.Status).NotEmpty().Must(x => new[] { "AVAILABLE", "EXPIRED", "INCARCERATED", "RETIRED" }.Contains(x));
                RuleFor(x => x.MainSkill).Must((command, mainSkill) =>
                    string.IsNullOrEmpty(mainSkill) || command.Skills != null && command.Skills.Any(s => s.Name == mainSkill))
                    .WithMessage("MainSkill must be one of the provided skills.");
            }
        }

        public class CommandHandler : IRequestHandler<Command, Result<CommandResponse>>
        {
            private readonly IValidator<Command> _validator;
            private readonly IConfiguration _configuration;
            private readonly IMapper _mapper;
            private readonly IMemberRepository _memberRepository;

            public CommandHandler(
                IValidator<Command> validator,
                IConfiguration configuration,
                IMapper mapper,
                IMemberRepository memberRepository)
            {
                _validator = validator;
                _configuration = configuration;
                _mapper = mapper;
                _memberRepository = memberRepository;
            }

            public async Task<Result<CommandResponse>> Handle(Command command, CancellationToken cancellationToken)
            {
                var validationResult = await _validator.ValidateAsync(command, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return new Result<CommandResponse>(new ValidationException("Validation Error", validationResult.Errors));
                }

                var model = _mapper.Map<Command, MemberModel>(command);
                
                var memberId = await _memberRepository.AddMemberAsync(model);

                if (memberId == null)
                {
                    return new CommandResponse
                    {
                        IsSuccess = false
                    };
                }

                return new CommandResponse
                {
                    MemberId = memberId,
                    IsSuccess = true
                };
            }
        }
    }
}
