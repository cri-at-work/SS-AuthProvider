using Funq;
using ServiceStack;
using ServiceStack.Auth;
using WebApplication1.Code;
using WebApplication1.ServiceInterface;

namespace WebApplication1
{
	public class AppHost : AppHostBase
	{
		/// <summary>
		/// Default constructor.
		/// Base constructor requires a name and assembly to locate web service classes. 
		/// </summary>
		public AppHost()
			: base("WebApplication1", typeof(MyServices).Assembly)
		{

		}

		/// <summary>
		/// Application specific configuration
		/// This method should initialize any IoC resources utilized by your web service classes.
		/// </summary>
		/// <param name="container"></param>
		public override void Configure(Container container)
		{
			
			Plugins.Add(new AuthFeature(() => new AuthUserSession(),
			  new IAuthProvider[] { 
        new UnityServiceAuthProvider()
      }));
		}
	}
}