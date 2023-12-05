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
            var jsonData = @"[
                {
                    ""company"": {
                        ""employees"": [
                            {
                                ""name"": ""Ursula Lane"",
                                ""country"": ""United States"",
                                ""region"": ""Zachodniopomorskie"",
                                ""postalZip"": ""6141"",
                                ""salary"": 3000
                            },
                            {
                                ""name"": ""Teegan Berg"",
                                ""country"": ""Vietnam"",
                                ""region"": ""Gangwon"",
                                ""postalZip"": ""13732"",
                                ""salary"": 5000
                            },
                            {
                                ""name"": ""Angelica Salinas"",
                                ""country"": ""France"",
                                ""region"": ""Lambayeque"",
                                ""postalZip"": ""5148"",
                                ""salary"": 4000
                            }
                        ]
                    }
                }
            ]";

            var data = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);

            Report1 report = new Report1();
            report.DataSource = data;
            report.SkipBlankPages = false;

            var header = new Telerik.Reporting.PageHeaderSection();
            header.Name = "header1";

            var numberTextBox = new Telerik.Reporting.TextBox();
            numberTextBox.Value = "=Fields.company.employees[?(@.salary == 4000)].name";
            numberTextBox.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            numberTextBox.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            numberTextBox.Width = Telerik.Reporting.Drawing.Unit.Inch(2);
            numberTextBox.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2);
            header.Items.Add(numberTextBox);

            //report.Items.Add(header); // Adding detail section to the report

            var reportProcessor = new ReportProcessor();

            var reportSource = new Telerik.Reporting.InstanceReportSource();
            reportSource.ReportDocument = report;

            RenderingResult result = reportProcessor.RenderReport("PDF", reportSource, null);

            if (!result.HasErrors)
            {
                string fileName = "Report5.pdf";
                string path = "C:\\Users\\31616\\Desktop\\c#\\TelerikReport-ConsoleApp-\\SavedPFD";
                string filePath = Path.Combine(path, fileName);

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                }

                Console.WriteLine("PDF has been successfully saved.");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine("Error: " + error.Message);
                }
                Console.WriteLine("An error occurred. The file was not saved.");
            }
        }
    }
}
