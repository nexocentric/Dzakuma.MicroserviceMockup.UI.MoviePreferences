using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Dzakuma.MicroserviceMockup.UI.MoviePreferences
{
	public static class TransportSafeString
	{
		public static string Encode(string originalString)
		{
			byte[] byteString = Encoding.UTF8.GetBytes(originalString);

			using (var inputStream = new MemoryStream(byteString))
			using (var outputStream = new MemoryStream())
			{
				using (var compressionStream = new GZipStream(outputStream, CompressionMode.Compress))
				{
					inputStream.CopyTo(compressionStream);
				}

				return Convert.ToBase64String(outputStream.ToArray());
			}
		}

		public static string Decode(string encodedString)
		{
			byte[] byteString = Convert.FromBase64String(encodedString);
			using (var inputStream = new MemoryStream(byteString))
			using (var outputStream = new MemoryStream())
			{
				using (var compressionStream = new GZipStream(inputStream, CompressionMode.Decompress))
				{
					compressionStream.CopyTo(outputStream);
				}

				return Encoding.UTF8.GetString(outputStream.ToArray());
			}
		}
	}
}
