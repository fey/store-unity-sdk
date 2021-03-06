﻿namespace Xsolla.Core
{
	public class WebRequestHeader
	{
		public string Name { get; set; }
		public string Value { get; set; }

		public static WebRequestHeader AuthHeader(string token)
		{
			return new WebRequestHeader {Name = "Authorization", Value = string.Format("Bearer {0}", token)};
		}
		
		public static WebRequestHeader AuthBasic(string value)
		{
			return new WebRequestHeader {Name = "Authorization", Value = string.Format("Basic {0}", value)};
		}
		
		public static WebRequestHeader ContentTypeHeader()
		{
			return new WebRequestHeader {Name = "Content-Type", Value = "application/json"};
		}
		
		public static WebRequestHeader AcceptHeader()
		{
			return new WebRequestHeader {Name = "Accept", Value = "application/json"};
		}
	}
}
