using System;

namespace LCSK
{
	public static class StringExtensions
	{
		public static string ToBase64(this string input)
		{
			byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(input);
			return Convert.ToBase64String(encbuff);
		}

		public static string FromBase64(this string input)
		{
			System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
			System.Text.Decoder utf8Decode = encoder.GetDecoder();

			byte[] todecode_byte = Convert.FromBase64String(input);
			int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
			char[] decoded_char = new char[charCount];
			utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
			string result = new String(decoded_char);
			return result;
		}
	}
}