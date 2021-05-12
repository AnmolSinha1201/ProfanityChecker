using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Service
{
	public class Success : BaseResponse
	{ }

	public class Failure : BaseResponse
	{ }

	public abstract class BaseResponse
	{
		public string Description;
		public string Status
		{
			get 
			{
				var attributes = this.GetType()
					.GetCustomAttributes(typeof(StatusText), true)
					.Cast<StatusText>()
					.ToList();

				if (attributes.Count > 0)
					return attributes.First().Text;

				return this.GetType().Name;
			}
		}

		public override string ToString()
		=> JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
	}


	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class StatusText : Attribute
	{
		public string Text;

		public StatusText(string Text)
		=> this.Text = Text;
	}
}