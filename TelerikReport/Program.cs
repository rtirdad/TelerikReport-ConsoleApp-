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
                                ""salary"": 3000,
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
            report.SkipBlankPages = false;
            report.DataSource = data;

            Telerik.Reporting.ReportHeaderSection Header = new Telerik.Reporting.ReportHeaderSection();

            Telerik.Reporting.TextBox headerTextBox = new Telerik.Reporting.TextBox();

            var EmployeeName = data[0]["company"]["employees"][0]["name"];
            headerTextBox.Value = EmployeeName.ToString();
            headerTextBox.Left = Telerik.Reporting.Drawing.Unit.Inch(3);
            headerTextBox.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            headerTextBox.Width = Telerik.Reporting.Drawing.Unit.Inch(2);
            headerTextBox.Height = Telerik.Reporting.Drawing.Unit.Inch(0);
            Header.Items.Add(headerTextBox);

            report.Items.Add(Header);



            Telerik.Reporting.DetailSection detail = new Telerik.Reporting.DetailSection();
            Telerik.Reporting.Panel panel1 = new Telerik.Reporting.Panel();
            Telerik.Reporting.TextBox detailTextbox = new Telerik.Reporting.TextBox();

            var postal = data[0]["company"]["employees"][0]["postalZip"];
            var salary = data[0]["company"]["employees"][0]["salary"];
            detailTextbox.Value = $"this employee resides in {postal.ToString()} and their salary is {salary.ToString()}/mo";
            detailTextbox.Left = Telerik.Reporting.Drawing.Unit.Inch(3);
            detailTextbox.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            detailTextbox.Width = Telerik.Reporting.Drawing.Unit.Inch(2);
            detailTextbox.Height = Telerik.Reporting.Drawing.Unit.Inch(0);
            detail.Items.Add(detailTextbox);

            // panel1
            panel1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.0, Telerik.Reporting.Drawing.UnitType.Cm));
            panel1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(8.5, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.5, Telerik.Reporting.Drawing.UnitType.Cm));
            panel1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;

            //panel1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { textBox1 });
            //detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { panel1 });/**/
            panel1.Items.Add(detailTextbox);
            detail.Items.Add(panel1);

            report.Items.Add(detail);


            Telerik.Reporting.ReportFooterSection footer = new Telerik.Reporting.ReportFooterSection();

            Telerik.Reporting.TextBox footerTextBox = new Telerik.Reporting.TextBox();
             footerTextBox.Value = "footer";

            var Region = data[0]["company"]["employees"][0]["region"];
            var Country = data[0]["company"]["employees"][0]["country"];
            footerTextBox.Value = $"{Region.ToString()} , {Country.ToString()}";
            footerTextBox.Left = Telerik.Reporting.Drawing.Unit.Inch(3);
            footerTextBox.Top = Telerik.Reporting.Drawing.Unit.Inch(0.5);
            footerTextBox.Width = Telerik.Reporting.Drawing.Unit.Inch(2);
            footerTextBox.Height = Telerik.Reporting.Drawing.Unit.Inch(0);
            footer.Items.Add(footerTextBox);
            report.Items.Add(footer);


            var reportProcessor = new ReportProcessor();

            var reportSource = new Telerik.Reporting.InstanceReportSource();
            reportSource.ReportDocument = report;

            RenderingResult result = reportProcessor.RenderReport("PDF", reportSource, null);

            if (!result.HasErrors)
            {
                string fileName = "Report2.pdf";
                string path = "C:\\Users\\CityGIS\\Desktop\\C#\\TelerikReport\\SavedPDF";
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