using First_Step.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;

namespace First_Step.Controllers
{
    public class EmployeeController : Controller
    {

        [HttpGet]
        public IActionResult Index(List<Employee> employee =  null)
        {
            employee = employee == null ? new List<Employee>() : employee;
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] IHostingEnvironment hostingEnviroment)
        {
            string fileName = $"{hostingEnviroment.WebRootPath}\\files\\{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            var emp = this.GetEmployeeList(file.FileName);
            return Index(emp);
        }

        private List<Employee> GetEmployeeList(string fileName)
        {
            List<Employee> employees = new List<Employee>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";", // Set the delimiter to semicolon
                MissingFieldFound = null // Ignore missing fields
            };

            // Path to the input CSV file
            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fileName;

            // Reading from the CSV file
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var employee = csv.GetRecord<Employee>();
                    employees.Add(employee);
                }
            }

            // Optionally, write to a new CSV file (if needed)
            var outputPath = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\FilesTo\NewFile.csv"}";
            using (var writer = new StreamWriter(outputPath))
            using (var csvWriter = new CsvWriter(writer, config))
            {
                csvWriter.WriteRecords(employees);
            }

            return employees;
        }

    }
}
