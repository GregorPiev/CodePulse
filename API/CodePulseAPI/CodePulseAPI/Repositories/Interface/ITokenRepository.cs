using Microsoft.AspNetCore.Identity;

namespace CodePulseAPI.Repositories.Interface
{
	public interface ITokenRepository
	{
		string CreateJwtToken(IdentityUser user, List<string> roles);
	}
}
