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

            Telerik.Reporting.PageHeaderSection Header1 = new PageHeaderSection();

            var headerTextBox = new Telerik.Reporting.TextBox();
            headerTextBox.Value = "=Fields.company.employees[0].name";
            headerTextBox.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            headerTextBox.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            headerTextBox.Width = Telerik.Reporting.Drawing.Unit.Inch(2);
            headerTextBox.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2);
            Header1.Items.Add(headerTextBox);
            //report.Items.Add(Header1);

            Telerik.Reporting.DetailSection Detail1 = new Telerik.Reporting.DetailSection();

            Telerik.Reporting.PageFooterSection footer = new PageFooterSection();


            
            var reportProcessor = new ReportProcessor();

            var reportSource = new Telerik.Reporting.InstanceReportSource();
            reportSource.ReportDocument = report;

            RenderingResult result = reportProcessor.RenderReport("PDF", reportSource, null);

            if (!result.HasErrors)
            {
                string fileName = "Report.pdf";
                string path = "C:\\Users\\CityGIS\\Desktop\\C#\\TelerikReport\\SavedPFD";
                string filePath = Path.Combine(path, fileName);

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                }

                Console.WriteLine("PDF has successfully saved :)");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine("Error: " + error.Message);
                }
                Console.WriteLine("An Error Occurred, the file was not saved :(");
            }
        }
    }
}
