using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Web;
using ServiceStack;

namespace WebApplication1.ServiceModel
{
	[Route("/hello")]
	public class Hello : IReturn<HelloResponse>
	{
	}

	public class HelloResponse
	{
		public string Result { get; set; }
	}
}