using System;
using ServiceStack;
using ServiceStack.Auth;
using WebApplication1.ServiceModel;

namespace WebApplication1.ServiceInterface
{
	public class UnityStsProxyService : Service
	{
		public GetTokenInfoResponse Any(GetTokenInfo request)
		{
			var response = new GetTokenInfoResponse
			{
				ClientIdentitifer = "UnityConsumer1",
				UserClaimIdentifier = "UnityUserName",
				Scope = "UnityScope1",
			};

			if (response == null)
			{
				throw HttpError.Unauthorized("Missing or invalid bearer token.");
			}

			var session = this.GetSession();

			PopulateSession(request.BearerToken, response, session);
			this.SaveSession(session, TimeSpan.FromMinutes(1));

			return response;
		}

		private static void PopulateSession(string bearerToken, GetTokenInfoResponse tokenInfo, IAuthSession session)//IUserAuthRepository authRepo, 
		{
			//var holdSessionId = session.Id;
			//session.PopulateWith(userAuth); //overwrites session.Id
			//session.Id = holdSessionId;
			session.Id = bearerToken;
			session.IsAuthenticated = true;
			session.UserAuthId = bearerToken;//userAuth.Id.ToString(CultureInfo.InvariantCulture);
			session.UserAuthName = tokenInfo.UserClaimIdentifier + "@" + tokenInfo.ClientIdentitifer + "@" + tokenInfo.Scope;
			session.UserName = tokenInfo.UserClaimIdentifier;
			//session.ProviderOAuthAccess = authRepo.GetUserAuthDetails(session.UserAuthId)
			//	 .ConvertAll(x => (IAuthTokens)x);
		}
	}
}