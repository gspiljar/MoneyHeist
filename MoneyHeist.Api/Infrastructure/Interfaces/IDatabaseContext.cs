namespace MoneyHeist.Api.Infrastructure.Interfaces
{
	public interface IDatabaseContext
	{
		string? GetDefaultConnectionString();
		void SetDefaultConnectionString(string conectionString);
	}
}
