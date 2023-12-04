using System;
using System.IO;
using System.Collections.Generic;
using Telerik.Reporting;
using Telerik.Reporting.Processing;
using Newtonsoft.Json;
using TelerikReport;

namespace TelerikReportingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
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

            var data = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);

            Report1 report = new Report1();
            report.DataSource = data;

            var reportProcessor = new ReportProcessor();
            var reportSource = new Telerik.Reporting.InstanceReportSource();
            reportSource.ReportDocument = report;

            RenderingResult result = reportProcessor.RenderReport("PDF", reportSource, null);

            if (!result.HasErrors)
            {
                string fileName = result.DocumentName + "." + result.Extension;
                string path = Path.GetTempPath();
                string filePath = Path.Combine(path, fileName);

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                }

                Console.WriteLine("PDF generated successfully at: " + filePath);
            }
            else
            {
                Console.WriteLine("Error occurred while generating the PDF.");
            }
        }
    }
}
