using System;
using System.Collections;
using System.IO;
using System.Linq;
using Telerik.Reporting;
using Telerik.Reporting.Processing;

namespace ConsoleApp2101
{
    class Program
    {
        static void Main(string[] args)
        {
            var reportSource = new UriReportSource();
            var processor = new ReportProcessor();
            var deviceInfo = new Hashtable();

            reportSource.Uri = @"C:\Program Files (x86)\Progress\Telerik Reporting R3 2023\Report Designer\Examples\MyReport.trdp";
            
            deviceInfo.Add("DocumentTitle", "SomeOptionalTitle");

            string[] availableFormats = new string[] { "PDF", "CSV", "DOCX", "XLSX", "PPTX", "RTF" };
            
            foreach (var format in availableFormats)
            {
                var result = processor.RenderReport(format, reportSource, deviceInfo);

                if (result.HasErrors)
                {
                    Console.WriteLine(string.Join(",", result.Errors.Select(s => s.Message)));
                }
                else
                {
                    File.WriteAllBytes($"MyReport.{format.ToLower()}", result.DocumentBytes);
                }
            }
            
            Console.WriteLine("Completed!");
            Console.ReadKey();
        }
    }
}