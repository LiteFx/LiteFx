using LiteFx.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Validation
{
	/// <summary>
	/// ResourceHelper should be used instead of Resource to obtain a translated text. 
	/// ResourceHelper obtain translation in ValidationHelper.ResourceManager or 
	/// Resource.ResourceManager if it doesn't find it in the first one.
	/// </summary>
	public static class ResourceHelper
	{
		/// <summary>
		/// GetString from ValidationHelper or Resource
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
