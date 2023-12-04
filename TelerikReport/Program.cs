
using System;
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
    
            string jsonData = File.ReadAllText("ExampleInfo.json");
            dynamic jsonObject = JsonConvert.DeserializeObject(jsonData);

            Report1 report = new Report1(); 

            report.ReportParameters["ParameterName"].Value = jsonObject.name;


            var reportProcessor = new ReportProcessor();

            var reportSource = new Telerik.Reporting.InstanceReportSource();
            reportSource.ReportDocument = report;


            object parameterValue = "Some Parameter Value";
            reportSource.Parameters.Add("ParameterName", parameterValue);

            RenderingResult result = reportProcessor.RenderReport("PDF", reportSource, null);

            if (!result.HasErrors)
            {
                string fileName = result.DocumentName + "." + result.Extension;
                string path = Path.GetTempPath();
                string filePath = Path.Combine(path, fileName);

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                }

                Console.WriteLine("PDF generated successfully at: " + filePath);
            }
            else
            {
                Console.WriteLine("Error occurred while generating the PDF.");
            }
        }
    }
}
