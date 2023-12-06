﻿using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Telerik.Reporting;
using Telerik.Reporting.Processing;
using TelerikReport;
using System.Linq;

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

            //Telerik.Reporting.Report report = new Telerik.Reporting.Report();
            Report1 report = new Report1();
            report.SkipBlankPages = false;
            report.DataSource = data;

            Telerik.Reporting.ReportHeaderSection reportHeader = new Telerik.Reporting.ReportHeaderSection();
            //Telerik.Reporting.PageHeaderSection reportHeader = new Telerik.Reporting.PageHeaderSection();
            //reportHeader.Name = "ReportHeaderSection";

            Telerik.Reporting.TextBox headerTextBox = new Telerik.Reporting.TextBox();
            headerTextBox.Value = "Ursula Lane3";
            //headerTextBox.Value = "=data.company.employees[1].name";
            //headerTextBox.Value = "=$.data.company.employees[1].name";
            headerTextBox.Left = Telerik.Reporting.Drawing.Unit.Inch(3);
            headerTextBox.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            headerTextBox.Width = Telerik.Reporting.Drawing.Unit.Inch(2);
            headerTextBox.Height = Telerik.Reporting.Drawing.Unit.Inch(0.5);
            reportHeader.Items.Add(headerTextBox);

            report.Items.Add(reportHeader);

            //Telerik.Reporting.DetailSection detail = new Telerik.Reporting.DetailSection();
            Telerik.Reporting.DetailSection detail = new Telerik.Reporting.DetailSection();
            detail.Name = "DetailSection";
            Telerik.Reporting.Panel panel1 = new Telerik.Reporting.Panel();
            Telerik.Reporting.TextBox detailTextBox = new Telerik.Reporting.TextBox();

            panel1.Left = Telerik.Reporting.Drawing.Unit.Inch(3);
            panel1.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            panel1.Width = Telerik.Reporting.Drawing.Unit.Inch(2);
            panel1.Height = Telerik.Reporting.Drawing.Unit.Inch(1);

            //detailTextBox.Value = "=data.company.employees[1].name";
            detailTextBox.Value = "detail info";
            detailTextBox.Left = Telerik.Reporting.Drawing.Unit.Inch(3);
            detailTextBox.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            detailTextBox.Width = Telerik.Reporting.Drawing.Unit.Inch(0);
            detailTextBox.Height = Telerik.Reporting.Drawing.Unit.Inch(0);

            //panel1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { detailTextBox });
           // detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { panel1 });
            /*detailTextBox.Value = "Detail infos"; 
            detailTextBox.Left = Telerik.Reporting.Drawing.Unit.Inch(3);
            detailTextBox.Top = Telerik.Reporting.Drawing.Unit.Inch(0.5); 
            detailTextBox.Width = Telerik.Reporting.Drawing.Unit.Inch(2);
            detailTextBox.Height = Telerik.Reporting.Drawing.Unit.Inch(2);
            /*detail.Items.Add(panel1);*/
            panel1.Items.Add(detailTextBox);

            detail.Items.Add(panel1);

            report.Items.Add(detail);


            Telerik.Reporting.ReportFooterSection footer = new Telerik.Reporting.ReportFooterSection();
            //footer.Name = "FooterSection";

            Telerik.Reporting.TextBox footerTextBox = new Telerik.Reporting.TextBox();
            //footerTextBox.Value = $".company.employees.name";
            footerTextBox.Value = "=data.company.employees[1].name";
            //footerTextBox.Value = "Test";
            footerTextBox.Left = Telerik.Reporting.Drawing.Unit.Inch(3);
            footerTextBox.Top = Telerik.Reporting.Drawing.Unit.Inch(0.2);
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