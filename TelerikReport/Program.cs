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
            Header1.Name = "$.company.employees.name[?(@.salary=4000)]";
            report.Items.Add(Header1);


            /*Header1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            var numberTextBox = new Telerik.Reporting.TextBox();

            numberTextBox.Value = "$.company.employee.name[?(@.salary=4000)]";
            numberTextBox.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            numberTextBox.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            numberTextBox.Width = Telerik.Reporting.Drawing.Unit.Inch(2);
            numberTextBox.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2);
            Header1.Items.Add(numberTextBox);
            report.Items.Add(Header1);


            Telerik.Reporting.DetailSection Detail1 = new Telerik.Reporting.DetailSection();
            Detail1.Name = "detail1";
            report.Items.Add((Telerik.Reporting.ReportItemBase)Detail1);
            report.Items.Add(Detail1);

            report.Width = Telerik.Reporting.Drawing.Unit.Inch(4);

            var Header1 = new Telerik.Reporting.PageHeaderSection();

            Header1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2);
            report.Items.Add(Header1);

            var numberTextBox = new Telerik.Reporting.TextBox();

            numberTextBox.Value = "$.company.employee[?(@.salary=4000)]";
            numberTextBox.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            numberTextBox.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            numberTextBox.Width = Telerik.Reporting.Drawing.Unit.Inch(2);
            numberTextBox.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2);
            Header1.Items.Add(numberTextBox);*/

            Telerik.Reporting.PageFooterSection FooterSection = new PageFooterSection();
          

            var reportProcessor = new ReportProcessor();

            var reportSource = new Telerik.Reporting.InstanceReportSource();
            reportSource.ReportDocument = report;

            RenderingResult result = reportProcessor.RenderReport("PDF", reportSource, null);

            if (!result.HasErrors)
            {
                string fileName = "Report1.pdf";
                string path = "C:\\Users\\CityGIS\\Desktop\\C#\\TelerikReport\\SavedPFD";
                string filePath = System.IO.Path.Combine(path, fileName);

                using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                {
                    fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                }

                Console.WriteLine("PDF is saved :)");
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
