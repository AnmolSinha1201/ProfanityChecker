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

		private string __status;
		public string Status
		{
			get 
			{
				if (__status != default)
					return __status;
				
				var attributes = this.GetType()
					.GetCustomAttributes(typeof(StatusText), true)
					.Cast<StatusText>()
					.ToList();

				if (attributes.Count > 0)
					return attributes.First().Text;

				return this.GetType().Name;
			}
			set
			{
				__status = value;
			}
		}
	}


	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class StatusText : Attribute
	{
		public string Text;

		public StatusText(string Text)
		=> this.Text = Text;
	}
}