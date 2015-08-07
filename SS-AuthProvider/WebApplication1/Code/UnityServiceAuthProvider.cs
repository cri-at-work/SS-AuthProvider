using System;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Web;
using WebApplication1.ServiceInterface;
using WebApplication1.ServiceModel;

namespace WebApplication1.Code
{
	public class UnityServiceAuthProvider : AuthProvider, IAuthWithRequest
	{
		public new static string Name = "UnityServiceAuthProvider";
		public new static string Realm = "Bearer";
		private string _authRealm;
		private string _provider;

		public UnityServiceAuthProvider()
		{
			_provider = Name;
			_authRealm = Realm;
			_provider = "UnitySTS";
		}

		public string AuthRealm
		{
			get { return _authRealm; }
			set { throw new NotSupportedException(); }
		}

		public override object Authenticate(IServiceBase authService, IAuthSession session, Authenticate request)
		{
			throw new NotSupportedException();
		}

		public string CallbackUrl
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override bool IsAuthorized(IAuthSession session, IAuthTokens tokens, Authenticate request = null)
		{
			if (request != null)
			{
				//if (!LoginMatchesSession(session, request.UserName))
				//{
				//	return false;
				//}
				throw new NotImplementedException();
			}

			return session != null && session.IsAuthenticated && !session.UserAuthName.IsNullOrEmpty();
		}

		public object Logout(IServiceBase service, Authenticate request)
		{
			throw new NotImplementedException();
		}

		public string Provider
		{
			get { return _provider; }
			set
			{
				var v = value;
				throw new NotSupportedException();
			}
		}

		protected static bool LoginMatchesSession(IAuthSession session, string bearerToken)
		{
			if (session == null || bearerToken == null) return false;
			if (!bearerToken.EqualsIgnoreCase(session.UserAuthId))
				return false;
			return true;
		}

		public void PreAuthenticate(IRequest req, IResponse res)
		{
			//Need to run SessionFeature filter since its not executed before this attribute (Priority -100)			
			SessionFeature.AddSessionIdToRequestFilter(req, res, null); //Required to get req.GetSessionId()
			
			var httpRequestBase = req.ToHttpRequestBase();
			var bearerToken = httpRequestBase.QueryString["access_token"] ?? "bearer-token-value";
			//var unityStsProxyService = new UnityStsProxyService();// req.TryResolve<UnityStsProxyService>();
			var unityStsProxyService = req.TryResolve<UnityStsProxyService>();
			unityStsProxyService.Request = req;
			req.SetSessionId(bearerToken);
			var session = req.GetSession();
			if (!session.IsAuthenticated)
			{
				unityStsProxyService.Any(new GetTokenInfo { BearerToken = bearerToken });
			}
		}
	}
}