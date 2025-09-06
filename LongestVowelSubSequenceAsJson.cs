using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

public static class Vowel
{
	public static string LongestVowelSubsequenceAsJson(List<string> words)
	{
		if (words == null || words.Count == 0)
		{
			return JsonSerializer.Serialize(new List<object>());
		}

		HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u',
											   'A', 'E', 'I', 'O', 'U' };

		var results = new List<object>();
		foreach (var word in words)
		{
			int maxLen = 0;
			int maxStart = -1;
			int currentLen = 0;
			int currentStart = -1;
			for (int i = 0; i < word.Length; i++)
			{
				if (vowels.Contains(word[i]))
				{
					if (currentLen == 0)
						currentStart = i;
					currentLen++;
				}
				else
				{
					if (currentLen > maxLen)
					{
						maxLen = currentLen;
						maxStart = currentStart;
					}
					currentLen = 0;
					currentStart = -1;
				}
			}
			// Son harf ünlü ise, son grubu kontrol et
			if (currentLen > maxLen)
			{
				maxLen = currentLen;
				maxStart = currentStart;
			}
			string seq = (maxLen > 0 && maxStart != -1) ? word.Substring(maxStart, maxLen) : "";
			results.Add(new
			{
				word = word,
				sequence = seq,
				length = seq.Length
			});
		}
		return JsonSerializer.Serialize(results);
	}

}