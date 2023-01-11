using IntegrationLibrary.Core.Model;
using IronPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Service.PDFGenerator
{
    public class TenderPDFReportGenerator
    {
        String html;

        public string CreatePDF(List<List<int>> bloodTypesByBanks, List<BloodBank> banks, List<int> bloodAmount, DateTime start, DateTime end)
        {
            var Renderer = new ChromePdfRenderer();

            CreatePDFStyle();
            CreatePDFBody(bloodTypesByBanks, banks, bloodAmount, start, end);
            CreatePieChart(bloodTypesByBanks, banks, bloodAmount);

            Renderer.RenderingOptions.EnableJavaScript = true;
            Renderer.RenderingOptions.RenderDelay = 1000;
            PdfDocument doc = Renderer.RenderHtmlAsPdf(html);

            String filename = CreateFileName();
            doc.SaveAs(filename);

            string path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + @"\IntegrationAPI\" + filename;
            return path;
        }
        public string CreateFileName()
        {
            string creationDate = DateTime.Now.ToString("ddMMyyyy");
            return "tender_blood_intake_report_" + creationDate + ".pdf";
        }
        public void CreatePDFBody(List<List<int>> bloodTypesByBanks, List<BloodBank> banks, List<int> bloodAmount, DateTime start, DateTime end)
        {
            html += "<body><h1>Tender blood intake report</h1><div>" +
                "<p>From: " + start.ToString("dd.MM.yyyy.") + "</p>" +
                "<p>Until: " + end.ToString("dd.MM.yyyy.") + "</p>";

            html += "<table class=\"GeneratedTable\"><thead><tr>" +
                                     " <th> Blood type </th>" +
                                     " <th> Quantity (units) </th> " +
                                    "  <th> Banks</th></tr></thead><tbody>";

            List<String> bloodTypes = CreateBloodTypeList();
            int index = 0;
            foreach (String bloodType in bloodTypes)
            {
                if (bloodAmount[index] > 0) { 
                    // do something with entry.Value or entry.Key
                    html += "<tr><td>" + bloodType + "</td><td>" + bloodAmount[index] + "</td><td>";

                    int bankIndex = 0;
                    switch (bloodType)
                    {
                        case "A positive":
                            bool isFirst = true;
                            foreach (BloodBank bank in banks)
                            {
                                if (!isFirst)
                                    html += "<br>";
                                if(bloodTypesByBanks[bankIndex][index] > 0 )
                                    html += bank.Name + "(" + bloodTypesByBanks[bankIndex][index] + " units)";
                                isFirst = false;
                                bankIndex++;
                            }

                            break;
                        case "B positive":
                            isFirst = true;
                            foreach (BloodBank bank in banks)
                            {
                                if (!isFirst)
                                    html += "<br>";
                                if (bloodTypesByBanks[bankIndex][index] > 0)
                                    html += bank.Name + "(" + bloodTypesByBanks[bankIndex][index] + " units)";
                                isFirst = false;
                                bankIndex++;
                            }

                            break;
                        case "AB positive":
                            isFirst = true;
                            foreach (BloodBank bank in banks)
                            {
                                if (!isFirst)
                                    html += "<br>";
                                if (bloodTypesByBanks[bankIndex][index] > 0)
                                    html += bank.Name + "(" + bloodTypesByBanks[bankIndex][index] + " units)";
                                isFirst = false;
                                bankIndex++;
                            }

                            break;
                        case "O positive":
                            isFirst = true;
                            foreach (BloodBank bank in banks)
                            {
                                if (!isFirst)
                                    html += "<br>";
                                if (bloodTypesByBanks[bankIndex][index] > 0)
                                    html += bank.Name + "(" + bloodTypesByBanks[bankIndex][index] + " units)";
                                isFirst = false;
                                bankIndex++;
                            }

                            break;
                        case "A negative":
                            isFirst = true;
                            foreach (BloodBank bank in banks)
                            {
                                if (!isFirst)
                                    html += "<br>";
                                if (bloodTypesByBanks[bankIndex][index] > 0)
                                    html += bank.Name + "(" + bloodTypesByBanks[bankIndex][index] + " units)";
                                isFirst = false;
                                bankIndex++;
                            }

                            break;
                        case "B negative":
                            isFirst = true;
                            foreach (BloodBank bank in banks)
                            {
                                if (!isFirst)
                                    html += "<br>";
                                if (bloodTypesByBanks[bankIndex][index] > 0)
                                    html += bank.Name + "(" + bloodTypesByBanks[bankIndex][index] + " units)";
                                isFirst = false;
                                bankIndex++;
                            }

                            break;
                        case "AB negative":
                            isFirst = true;
                            foreach (BloodBank bank in banks)
                            {
                                if (!isFirst)
                                    html += "<br>";
                                if (bloodTypesByBanks[bankIndex][index] > 0)
                                    html += bank.Name + "(" + bloodTypesByBanks[bankIndex][index] + " units)";
                                isFirst = false;
                                bankIndex++;
                            }

                            break;
                        case "O negative":
                            isFirst = true;
                            foreach (BloodBank bank in banks)
                            {
                                if (!isFirst)
                                    html += "<br>";
                                if (bloodTypesByBanks[bankIndex][index] > 0)
                                    html += bank.Name + "(" + bloodTypesByBanks[bankIndex][index] + " units)";
                                isFirst = false;
                                bankIndex++;
                            }

                            break;
                        default:
                            //asd
                            break;
                    }
                    html += @"</td></tr>";
                    index++;
                }
            }

            html += @"</tbody></table>";
        }
        private List<String> CreateBloodTypeList()
        {
            List<String> bloodTypeList = new List<String>();
            bloodTypeList.Add("A positive");
            bloodTypeList.Add("B positive");
            bloodTypeList.Add("AB positive");
            bloodTypeList.Add("O positive");
            bloodTypeList.Add("A negative");
            bloodTypeList.Add("B negative");
            bloodTypeList.Add("AB negative");
            bloodTypeList.Add("O negative");

            return bloodTypeList;
        }
        public void CreatePieChart(List<List<int>> bloodTypesByBanks, List<BloodBank> banks, List<int> bloodAmount)
        {
            html += @"<div id='chart' style='width: 950px;'></div>
                    <script src='https://d3js.org/d3.v4.js'></script>
                    <!-- Load c3.css -->
                    <link href='https://cdnjs.cloudflare.com/ajax/libs/c3/0.5.4/c3.css' rel='stylesheet'>
                    <!-- Load d3.js and c3.js -->
                    <script src='https://code.highcharts.com/highcharts.js'></script>";
            html += "<h2>Blood Type Pie Chart</h2>";
            html += "<div id=\"container\" style = \"height: 816px; width: 1056px; \"></div>";

            html += @"<script>
                                  Highcharts.chart('container', {
                      chart: {
                          plotBackgroundColor: null,
                          plotBorderWidth: null,
                          plotShadow: false,
                          type: 'pie'
                      },
                      title: {
                          text: 'Blood Type Pie Chart'
                      },
                      tooltip: {
                          pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                      },
                      plotOptions: {
                          pie: {
                              allowPointSelect: true,
                              cursor: 'pointer',
                              dataLabels: {
                                  enabled: false
                              },
                              showInLegend: true
                          }
                      },
                      series: [{
                          name: 'Brands',
                          colorByPoint: true,
                          data: [";

            bool isFirst = true;

            int maxValue = SumBlood(bloodAmount);
            
            int index = 0;
            foreach (String bloodType in CreateBloodTypeList())
            {
                if (bloodAmount[index] > 0)
                {
                    double value = ((double)bloodAmount[index] * 100) / maxValue;
                    if (!isFirst)
                        html += ",";
                    html += "{name: '" + bloodType + "', y: " + Math.Round(value, 2) + "}";
                    isFirst = false;
                    index++;
                }
            }
                    html += @"]
                          }]
                      });
                    </script>

                    </body>
                </html>";
        }
        private int SumBlood(List<int> bloodAmount)
        {
            int sum = 0;
            foreach (int amount in bloodAmount)
            {
                sum += amount;
            }
            return sum;
        }
        public void CreatePDFStyle()
        {
            html = @"<head>
                        <meta charset='utf-8' />
                        <title>Emergency Blood Report PDF</title>
                        <style>
                            table.GeneratedTable {
                                width: 100 %; 
                                background-color: #ffffff; 
                                border-collapse: collapse;
                                border-width: 2px; 
                                border-color: #ff5353; 
                                border-style: solid; 
                                color: #000000; } 
                            table.GeneratedTable td, table.GeneratedTable th { 
                                border-width: 2px; 
                                border-color: #ff5353; 
                                border-style: solid; 
                                padding: 3px;}
                            table.GeneratedTable thead { 
                                background-color: #ff5353; }
                        </style>
                    </head>";
        }
        private String GetBloodTypeAsString(BloodType type)
        {
            switch (type)
            {
                case BloodType.ABP: return "AB positive";
                case BloodType.ABN: return "AB negative";
                case BloodType.AP: return "A positive";
                case BloodType.AN: return "A negative";
                case BloodType.BP: return "B positive";
                case BloodType.BN: return "B negative";
                case BloodType.OP: return "O positive";
                case BloodType.ON: return "O negative";
            }
            return "";
        }
    }
}
