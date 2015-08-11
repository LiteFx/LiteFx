
namespace LiteFx.DomainResult
{
	public interface IDomainResult
	{
	}

	public interface IDomainResult<T> : IDomainResult
	{
		T Body { get; }
	}
}
