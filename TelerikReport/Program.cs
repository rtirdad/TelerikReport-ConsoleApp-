using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Telerik.Reporting;
using Telerik.Reporting.Processing;
using TelerikReport;
using System.Security.Cryptography.X509Certificates;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
            //report.Items.Add(Header1);

            var headerTextBox = new Telerik.Reporting.TextBox();
            headerTextBox.Value = "=Fields.company.employees[1].name";
            headerTextBox.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            headerTextBox.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            headerTextBox.Width = Telerik.Reporting.Drawing.Unit.Inch(2);
            headerTextBox.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2);
            //Header1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { headerTextBox });
            //Header1.Items.Add(headerTextBox);
            //report.Items.Add(Header1 );


            Telerik.Reporting.DetailSection Detail1 = new Telerik.Reporting.DetailSection();
            Detail1.Name = "detail1";
            report.Items.Add((Telerik.Reporting.ReportItemBase)Detail1);
            report.Items.Add(Detail1);
            report.Width = Telerik.Reporting.Drawing.Unit.Inch(4);
            report.Items.Add(Header1);
            var numberTextBox = new Telerik.Reporting.TextBox();

            numberTextBox.Value = "$.company.employee[?(@.salary=4000)]";
            numberTextBox.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            numberTextBox.Top = Telerik.Reporting.Drawing.Unit.Inch(0);
            numberTextBox.Width = Telerik.Reporting.Drawing.Unit.Inch(2);
            numberTextBox.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2);
            Detail1.Items.Add(numberTextBox); 
            report.Items.Add(Detail1);

            Telerik.Reporting.PageFooterSection footer = new PageFooterSection();
            var footerTextBox = new Telerik.Reporting.TextBox();
            footerTextBox.Value = "Footer Details";
            footer.Name = footerTextBox.Value;
            //report.Items.Add(footer);
            
         


            var reportProcessor = new ReportProcessor();

            var reportSource = new Telerik.Reporting.InstanceReportSource();
            reportSource.ReportDocument = report;

            RenderingResult result = reportProcessor.RenderReport("PDF", reportSource, null);

            if (!result.HasErrors)
            {
                string fileName = "Report5.pdf";
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

        private void detail_ItemDataBinding(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.DetailSection section = (sender as Telerik.Reporting.Processing.DetailSection);
            Telerik.Reporting.Processing.TextBox txt = (Telerik.Reporting.Processing.TextBox)Telerik.Reporting.Processing.ElementTreeHelper.GetChildByName(section, "textBox1");
            object title = section.DataObject["Title"];
            if ((string)title == "Developer")
            {
                txt.Style.BackgroundColor = System.Drawing.Color.Blue;
            }
        }
    }
}

/*var jsonData = @"[
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
            ]";*/