using FluentValidation;
using LanguageExt.Common;
using MediatR;

namespace MoneyHeist.Api.ApiLogic.Members
{
	public class AddMemberHandler
    {
		public class Command : IRequest<Result<CommandResponse>>
		{
		}

		public class CommandResponse
		{
			public bool IsSuccess { get; set; }
		}

		public class CommandValidator : AbstractValidator<Command>
		{
			public readonly IConfiguration _configuration;
			public CommandValidator(IConfiguration configuration)
			{
				_configuration = configuration;
			}
		}

		public class CommandHandler : IRequestHandler<Command, Result<CommandResponse>>
		{
			private readonly IValidator<Command> _validator;
			private readonly IConfiguration _configuration;

			public CommandHandler(
				IValidator<Command> validator,
				IConfiguration configuration)
			{
				_validator = validator;
				_configuration = configuration;
			}

			public async Task<Result<CommandResponse>> Handle(Command command, CancellationToken cancellationToken)
			{
				return new CommandResponse
				{
					IsSuccess = true
				};
			}
		}
	}
}
