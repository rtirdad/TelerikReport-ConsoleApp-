
using System;
using System.Collections.Generic;
using System.IO;
using Telerik.Reporting;
using Telerik.Reporting.Processing;
using Newtonsoft.Json;
using TelerikReport;
using System.ComponentModel.Composition.Primitives;

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

            Report1 reportName = new Report1();
            reportName.DataSource = data;


            /*ReportProcessor reportProcessor = new ReportProcessor();
            RenderingResult result = reportProcessor.RenderReport("PDF", report, null);

            if (result.DocumentBytes != null && result.DocumentBytes.Length > 0)
            {
                //string outputPath = "output/path/output.pdf";
                string outputPath = "c#/TelerikReport/output.";
                System.IO.File.WriteAllBytes(outputPath, result.DocumentBytes);
                Console.WriteLine("PDF generated successfully!");
            }
            else
            {
                Console.WriteLine("Error: No document bytes generated.");
            }*/
            var reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();

            // set any deviceInfo settings if necessary
            var deviceInfo = new System.Collections.Hashtable();

            // Depending on the report definition choose ONE of the following REPORT SOURCES
            //                  -1-
            // ***CLR (CSharp) report definitions***
            var reportSource = new Telerik.Reporting.TypeReportSource();

            // reportName is the Assembly Qualified Name of the report
            //reportSource.TypeName = reportName;
            //                  -1-

            ////                  -2-
            //// ***Declarative (TRDP/TRDX) report definitions***
            //var reportSource = new Telerik.Reporting.UriReportSource();

            //// reportName is the path to the TRDP/TRDX file
            //reportSource.Uri = reportName;
            ////                  -2-

            ////                  -3-
            //// ***Instance of the report definition***
            //var reportSource = new Telerik.Reporting.InstanceReportSource();

            //// Report1 is the class of the report. It should inherit Telerik.Reporting.Report class
            //reportSource.ReportDocument = new Report1();
            ////                  -3-

            // Pass parameter value with the Report Source if necessary
            object parameterValue = "Some Parameter Value";
            reportSource.Parameters.Add("ParameterName", parameterValue);

            //Telerik.Reporting.Processing.RenderingResult result = reportProcessor.RenderReport("PDF", reportSource, deviceInfo);
            RenderingResult result = reportProcessor.RenderReport("PDF", reportName, null);

            if (!result.HasErrors)
            {
                string fileName = result.DocumentName + "." + result.Extension;
                string path = System.IO.Path.GetTempPath();
                string filePath = System.IO.Path.Combine(path, fileName);

                using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                {
                    fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                }
            }
        }
    }
}