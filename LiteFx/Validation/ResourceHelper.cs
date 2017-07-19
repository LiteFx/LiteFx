using LiteFx.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Validation
{
	/// <summary>
	/// 
	/// </summary>
	public static class ResourceHelper
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static string GetString(string name)
		{
			string validationMessage = ValidationHelper.ResourceManager.GetString(name);

			if (!string.IsNullOrEmpty(validationMessage))
				return validationMessage;

			return Resources.ResourceManager.GetString(name);
		}
	}
}
