// COPIED FROM https://github.com/paulcbetts/mvvmlight/blob/master/GalaSoft.MvvmLight/GalaSoft.MvvmLight.Extras%20(NET35)/Ioc/PreferredConstructor.cs
using System;

namespace FluiTec.AppFx.InversionOfControl.SimpleIoC
{
	[AttributeUsage(AttributeTargets.Constructor)]
	public sealed class PreferredConstructorAttribute : Attribute
	{
	}
}