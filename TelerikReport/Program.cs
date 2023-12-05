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

            // Create a report
            //Telerik.Reporting.Report report = new Telerik.Reporting.Report();
            Report1 report = new Report1();
            report.SkipBlankPages = false;

            // Define the DataSource for the report
            report.DataSource = data;

            // Create a Report Header section
            Telerik.Reporting.ReportHeaderSection reportHeader = new Telerik.Reporting.ReportHeaderSection();
            reportHeader.Name = "ReportHeaderSection";

            // Create a TextBox in the Report Header to display the name "Ursula Lane"
            var headerTextBox = new Telerik.Reporting.TextBox();
            //headerTextBox.Value = "Ursula Lane";
            headerTextBox.Value = "=Fields.company.employees[?(@.salary == 4000)].name";
            headerTextBox.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            headerTextBox.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            headerTextBox.Width = Telerik.Reporting.Drawing.Unit.Inch(2);
            headerTextBox.Height = Telerik.Reporting.Drawing.Unit.Inch(0.5);
            reportHeader.Items.Add(headerTextBox);

            // Add the Report Header section to the report
            report.Items.Add(reportHeader);

            // Create a Detail section for the report
            Telerik.Reporting.DetailSection detail = new Telerik.Reporting.DetailSection();
            detail.Name = "DetailSection";

            // Create a TextBox in the Detail section to display employee names
            var textBox = new Telerik.Reporting.TextBox();
            textBox.Value = "=Fields.name"; // Displaying employee names
            textBox.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            textBox.Top = Telerik.Reporting.Drawing.Unit.Inch(0.5); // Adjust position below header
            textBox.Width = Telerik.Reporting.Drawing.Unit.Inch(2);
            textBox.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2);
            detail.Items.Add(textBox);

            // Add the Detail section to the report
            //report.Items.Add(detail);

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