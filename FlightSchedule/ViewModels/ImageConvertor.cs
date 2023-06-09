﻿using Avalonia.Data.Converters;
using Avalonia.Platform;
using Avalonia;
using System;
using System.Globalization;
using System.Reflection;
using Avalonia.Media.Imaging;

namespace FlightSchedule.ViewModels
{
	public class ImageConvertor : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (value is string picture && targetType.IsAssignableFrom(typeof(Bitmap)))
			{
				Uri uri;

				if (picture.StartsWith("avares://"))
				{
					uri = new Uri(picture);
				}
				else
				{
					string assemblyName = Assembly.GetEntryAssembly().GetName().Name;
					uri = new Uri($"avares://{assemblyName}/{picture}");
				}
				var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
				var asset = assets.Open(uri);

				return new Bitmap(asset);
			}
			throw new NotImplementedException();
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
