using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public static class EmployeeAnalysis
{
	public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
	{
		var filtered = employees
			.Where(e => e.Age >= 25 && e.Age <= 40)
			.Where(e => e.Department == "IT" || e.Department == "Finance")
			.Where(e => e.Salary >= 5000 && e.Salary <= 9000)
			.Where(e => e.HireDate.Year > 2017)
			.ToList();

		var names = filtered
			.Select(e => e.Name)
			.OrderByDescending(n => n.Length)
			.ThenBy(n => n)
			.ToList();

		var totalSalary = Math.Round(filtered.Sum(e => e.Salary), 2);
		var averageSalary = filtered.Count > 0 ? Math.Round(filtered.Average(e => e.Salary), 2) : 0;
		var minSalary = filtered.Count > 0 ? Math.Round(filtered.Min(e => e.Salary), 2) : 0;
		var maxSalary = filtered.Count > 0 ? Math.Round(filtered.Max(e => e.Salary), 2) : 0;
		var count = filtered.Count;

		var result = new
		{
			Names = names,
			TotalSalary = totalSalary,
			AverageSalary = averageSalary,
			MinSalary = minSalary,
			MaxSalary = maxSalary,
			Count = count
		};

		return JsonSerializer.Serialize(result, new JsonSerializerOptions
		{
			WriteIndented = true,
			Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
		});
	}
}