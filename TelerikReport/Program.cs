
using System;
using System.Collections.Generic;
using System.IO;
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
            var jsonData = @"[
                {
                    ""store"": {
                        ""book"": [
                            {
                                ""category"": ""reference"",
                                ""author"": ""Nigel Rees"",
                                ""title"": ""Sayings of the Century"",
                                ""price"": 8.95
                            },
                            {
                                ""category"": ""fiction"",
                                ""author"": ""Evelyn Waugh"",
                                ""title"": ""Sword of Honour"",
                                ""price"": 12.99
                            }
                        ]
                    }
                }
            ]";

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(jsonData);

            Report1 report = new Report1();
            report.DataSource = data;


            ReportProcessor reportProcessor = new ReportProcessor();
            RenderingResult result = reportProcessor.RenderReport("PDF", report, null);

            if (result.DocumentBytes != null && result.DocumentBytes.Length > 0)
            {
                //string outputPath = "output/path/output.pdf";
                string outputPath = "c#/TelerikReport/output";
                System.IO.File.WriteAllBytes(outputPath, result.DocumentBytes);
                Console.WriteLine("PDF generated successfully!");
            }
            else
            {
                Console.WriteLine("Error: No document bytes generated.");
            }

        }
    }
}