using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

public static class SubArray
{
	public static string MaxIncreasingSubArrayAsJson(List<int> numbers)
	{
		if (numbers == null || numbers.Count == 0)
		{
			return JsonSerializer.Serialize(new List<int>());
		}
		List<int> maxSubArray = new List<int>();
		List<int> currentSubArray = new List<int> { numbers[0] };
		for (int i = 1; i < numbers.Count; i++)
		{
			if (numbers[i] > numbers[i - 1])
			{
				currentSubArray.Add(numbers[i]);
			}
			else
			{
				if (currentSubArray.Count > maxSubArray.Count)
				{
					maxSubArray = new List<int>(currentSubArray);
				}
				else if (currentSubArray.Count == maxSubArray.Count && currentSubArray.Count > 0)
				{
					// Ayný uzunlukta ise sonuncusunu seç
					maxSubArray = new List<int>(currentSubArray);
				}
				currentSubArray = new List<int> { numbers[i] };
			}
		}
		if (currentSubArray.Count > maxSubArray.Count)
		{
			maxSubArray = currentSubArray;
		}
		else if (currentSubArray.Count == maxSubArray.Count && currentSubArray.Count > 0)
		{
			maxSubArray = currentSubArray;
		}
		return JsonSerializer.Serialize(maxSubArray);
	}
}