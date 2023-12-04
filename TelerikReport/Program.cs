using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Telerik.Reporting;
using Telerik.Reporting.Processing;
using TelerikReport;

namespace TelerikReportingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Your JSON input
            string jsonData = @"
            [
                {
                    ""name"": ""Ursula Lane"",
                    ""country"": ""United States"",
                    ""region"": ""Zachodniopomorskie"",
                    ""postalZip"": ""6141""
                },
                {
                    ""name"": ""Gail Underwood"",
                    ""country"": ""Ukraine"",
                    ""region"": ""Champagne-Ardenne"",
                    ""postalZip"": ""8472 CK""
                },
                {
                    ""name"": ""Teegan Berg"",
                    ""country"": ""Vietnam"",
                    ""region"": ""Gangwon"",
                    ""postalZip"": ""13732""
                },
                {
                    ""name"": ""Arsenio Ewing"",
                    ""country"": ""United States"",
                    ""region"": ""Guanacaste"",
                    ""postalZip"": ""41623""
                },
                {
                    ""name"": ""Angelica Salinas"",
                    ""country"": ""France"",
                    ""region"": ""Lambayeque"",
                    ""postalZip"": ""5148""
                }
            ]";

            // Deserialize JSON to a list of dynamic objects
            var data = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);

            // Create a new report instance
            Report1 report = new Report1();

            // Set the report data source
            report.DataSource = data;

            // Create a ReportProcessor instance
            var reportProcessor = new ReportProcessor();

            // Create an InstanceReportSource and assign the report to it
            var reportSource = new Telerik.Reporting.InstanceReportSource();
            reportSource.ReportDocument = report;

            // Render the report to a PDF
            RenderingResult result = reportProcessor.RenderReport("PDF", reportSource, null);

            // Check if rendering was successful
            if (!result.HasErrors)
            {
                // Specify the file name
                string fileName = "GeneratedReport.pdf";

                // Set the file path where you want to save the PDF
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                // Write the PDF content to disk
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                }

                Console.WriteLine("PDF generated successfully. File saved to: " + filePath);
            }
            else
            {
                // Output specific errors, if any
                foreach (var error in result.Errors)
                {
                    Console.WriteLine("Error: " + error.Message);
                }
                Console.WriteLine("An Error Occurred while generating the PDF.");
            }
        }
    }
}
