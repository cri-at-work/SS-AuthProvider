using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using WebApplication1.ServiceModel;

namespace WebApplication1.ServiceInterface
{
	public class MyServices : Service
	{
		[Authenticate]
		public object Any(Hello request)
		{
			var session = GetSession();
			return new HelloResponse { Result = "Hello, {0}!".Fmt(session.UserAuthName) };
		}
	}
}