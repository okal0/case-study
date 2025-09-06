using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;



public static class Filter
{
	public static string FilterPeopleFromXml(string xml)
	{
		var doc = XDocument.Parse(xml);
		var people = doc.Descendants("Person")
			.Select(x => new {
				Name = (string)x.Element("Name"),
				Age = int.Parse((string)x.Element("Age")),
				Department = (string)x.Element("Department"),
				Salary = decimal.Parse((string)x.Element("Salary")),
				HireDate = DateTime.Parse((string)x.Element("HireDate"), CultureInfo.InvariantCulture)
			})
			.Where(p => p.Age > 30
				&& p.Department == "IT"
				&& p.Salary > 5000
				&& p.HireDate.Year < 2019)
			.ToList();

		var result = new
		{
			Names = people.Select(p => p.Name).OrderBy(n => n).ToList(),
			TotalSalary = people.Sum(p => p.Salary),
			AverageSalary = people.Count > 0 ? people.Average(p => p.Salary) : 0,
			MaxSalary = people.Count > 0 ? people.Max(p => p.Salary) : 0,
			Count = people.Count
		};

		return JsonSerializer.Serialize(result);
	}
}