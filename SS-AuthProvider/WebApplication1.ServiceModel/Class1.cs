
namespace WebApplication1.ServiceModel
{
	public class GetTokenInfo
	{
		public string BearerToken { get; set; }
	}

	public class GetTokenInfoResponse
	{
		public string UserClaimIdentifier { get; set; }
		public string ClientIdentitifer { get; set; }
		public string Scope { get; set; }
	}
}
