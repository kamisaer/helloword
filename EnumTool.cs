using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumTool 
{
	public static string ToIntString<T>(this T enumValue) where T : Enum
	{
		var value =Convert.ChangeType(enumValue, typeof(int));
		return ((int)value).ToString();
	}
	public static T ToParseEnum<T>(this string enumValue) where T : Enum
	{
		T t = (T)Enum.Parse(typeof(T), enumValue, true);
		return t;

	}
}
