using IntegrationLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using IronPdf;

namespace IntegrationLibrary.Core.Service.PDFGenerator
{
    public class EmergencyRequestPDFGenerator
    {
        String html;

        public string CreatePDF(EmergencyBloodReportParams reportParams, EmergencyBloodReport report)
        {
            var Renderer = new ChromePdfRenderer();

            CreatePDFStyle();
            CreatePDFBody(reportParams, report);
            CreatePieChart(report);
            CreateStackChart(report);

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
            return "emergency_blood_intake_report_" + creationDate + ".pdf";
        }
        public void CreatePDFBody(EmergencyBloodReportParams reportParams, EmergencyBloodReport report)
        {
            html += "<body><h1>Emergency blood intake report</h1><div>" +
                "<p>From: " + reportParams.StartDate.ToString("dd.MM.yyyy.") + "</p>" +
                "<p>Until: " + reportParams.EndDate.ToString("dd.MM.yyyy.") + "</p>";
            
            html += "<table class=\"GeneratedTable\"><thead><tr>" +
                                     " <th> Blood type </th>" +
                                     " <th> Quantity (units) </th> " +
                                    "  <th> Banks</th></tr></thead><tbody>";


            foreach (KeyValuePair<BloodType, int> entry in report.BloodAmmounts)
            {
                // do something with entry.Value or entry.Key
                html += "<tr><td>" + GetBloodTypeAsString(entry.Key) + "</td><td>" + entry.Value + "</td><td>";

                switch (entry.Key)
                {
                    case BloodType.ABP:
                        bool isFirst = true;
                        foreach (KeyValuePair<string, int> bankEntry in report.ABPBanks)
                        {
                            if (!isFirst)
                                html += "<br>";
                            html += bankEntry.Key + "(" + bankEntry.Value + " units)";
                            isFirst = false;
                        }
                        
                        break;
                    case BloodType.ABN:
                        isFirst = true;
                        foreach (KeyValuePair<string, int> bankEntry in report.ABNBanks)
                        {
                            if (!isFirst)
                                html += "<br>";
                            html += bankEntry.Key + "(" + bankEntry.Value + " units)";
                            isFirst = false;
                        }
                        break;
                    case BloodType.AP:
                        isFirst = true;
                        foreach (KeyValuePair<string, int> bankEntry in report.APBanks)
                        {
                            if (!isFirst)
                                html += "<br>";
                            html += bankEntry.Key + "(" + bankEntry.Value + " units)";
                            isFirst = false;
                        }
                        break;
                    case BloodType.AN:
                        isFirst = true;
                        foreach (KeyValuePair<string, int> bankEntry in report.ANBanks)
                        {
                            if (!isFirst)
                                html += "<br>";
                            html += bankEntry.Key + "(" + bankEntry.Value + " units)";
                            isFirst = false;
                        }
                        break;
                    case BloodType.BP:
                        isFirst = true;
                        foreach (KeyValuePair<string, int> bankEntry in report.BPBanks)
                        {
                            if (!isFirst)
                                html += "<br>";
                            html += bankEntry.Key + "(" + bankEntry.Value + " units)";
                            isFirst = false;
                        }
                        break;
                    case BloodType.BN:
                        isFirst = true;
                        foreach (KeyValuePair<string, int> bankEntry in report.BNBanks)
                        {
                            if (!isFirst)
                                html += "<br>";
                            html += bankEntry.Key + "(" + bankEntry.Value + " units)";
                            isFirst = false;
                        }
                        break;
                    case BloodType.OP:
                        isFirst = true;
                        foreach (KeyValuePair<string, int> bankEntry in report.OPBanks)
                        {
                            if (!isFirst)
                                html += "<br>";
                            html += bankEntry.Key + "(" + bankEntry.Value + " units)";
                            isFirst = false;
                        }
                        break;
                    case BloodType.ON:
                        isFirst = true;
                        foreach (KeyValuePair<string, int> bankEntry in report.ONBanks)
                        {
                            if (!isFirst)
                                html += "<br>";
                            html += bankEntry.Key + "(" + bankEntry.Value + " units)";
                            isFirst = false;
                        }
                        break;
                    default:
                        //asd
                        break;
                }
                html += @"</td></tr>";

            }
            
            html += @"</tbody></table>";
        }
        public void CreatePieChart(EmergencyBloodReport report)
        {
            html += @"<div id='chart' style='width: 950px;'></div>
                    <script src='https://d3js.org/d3.v4.js'></script>
                    <!-- Load c3.css -->
                    <link href='https://cdnjs.cloudflare.com/ajax/libs/c3/0.5.4/c3.css' rel='stylesheet'>
                    <!-- Load d3.js and c3.js -->
                    <script src='https://code.highcharts.com/highcharts.js'></script>";
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

            int maxValue = SumBlood(report.BloodAmmounts);
            foreach (KeyValuePair<BloodType, int> entry in report.BloodAmmounts)
            {
                double value = ((double)entry.Value * 100) / maxValue;
                if (!isFirst)
                    html += ",";
                html += "{name: '" + GetBloodTypeAsString(entry.Key) + "', y: " + Math.Round(value, 2) + "}";
                isFirst = false;

            }
                
                html += @"]
                          }]
                      });
                    </script>";
            }
        private int SumBlood(Dictionary<BloodType, int> report)
        {
            int sum = 0;
            foreach (KeyValuePair<BloodType, int> entry in report)
            {
                sum += entry.Value;
            }
            return sum;
        }
        public void CreateStackChart(EmergencyBloodReport report)
        {
            html += @"<div id='chart' style='width: 950px;'></div>";
            html += "<br><br><br><figure class=\"highcharts-figure\"><div id =\"containeer\"></div></figure>";

            html += @"<script>
                            Highcharts.chart('containeer', {
                    chart:
                            {
                            type: 'column'
                    },
                    title:
                            {
                            text: 'Blood quantity by blood type and blood bank',
                        align: 'left'
                    },
                    xAxis:
                            {
                            categories: ['A positive', 'B positive', 'AB positive', 'O positive', 'A negative', 'B negative', 'AB negative', 'O negative']
                    },
                    yAxis:
                            {
                            min: 0,
                        title:
                                {
                                text: 'Blood quantity'
                        },
                        stackLabels:
                                {
                                enabled: true,
                            style:
                                    {
                                    fontWeight: 'bold',
                                color: ( // theme
                                    Highcharts.defaultOptions.title.style &&
                                    Highcharts.defaultOptions.title.style.color
                                ) || 'gray',
                                textOutline: 'none'
                            }
                                }
                            },
                    legend:
                            {
                            align: 'left',
                        x: 40,
                        verticalAlign: 'top',
                        y: 50,
                        floating: true,
                        backgroundColor:
                                Highcharts.defaultOptions.legend.backgroundColor || 'white',
                        borderColor: '#CCC',
                        borderWidth: 1,
                        shadow: false
                    },
                    tooltip:
                            {
                            headerFormat: '<b>{point.x}</b><br/>',
                        pointFormat: '{series.name}: {point.y}<br/>Total: {point.stackTotal}'
                    },
                    plotOptions:
                            {
                            column:
                                {
                                stacking: 'normal',
                            dataLabels:
                                    {
                                    enabled: true
                            }
                                }
                            },
                    series:
                            [";

            bool isFirst = true;
            List<String> banks = GetBanksFromReport(report);
            foreach (String bank in banks)
            {
                int a = -1;
                if (!isFirst)
                    html += ",";
                html += "{name: '" + bank + "', data: [" + (report.APBanks.TryGetValue(bank, out a) ? a : 0) + ", " +
                                        (report.BPBanks.TryGetValue(bank, out a) ? a : 0) + ", " +
                                        (report.ABPBanks.TryGetValue(bank, out a) ? a : 0) + ", " +
                                        (report.OPBanks.TryGetValue(bank, out a) ? a : 0) + ", " +
                                        (report.ANBanks.TryGetValue(bank, out a) ? a : 0) + ", " +
                                        (report.BNBanks.TryGetValue(bank, out a) ? a : 0) + ", " +
                                        (report.ABNBanks.TryGetValue(bank, out a) ? a : 0) + ", " +
                                        (report.ONBanks.TryGetValue(bank, out a) ? a : 0) + "]}";
                isFirst = false;
            }
            html += @"]
                      });
                    </script>

                    </body>
                </html>";
        }
        private List<String> GetBanksFromReport(EmergencyBloodReport report)
        {
            List<String> banks = new List<String>();
            foreach (KeyValuePair<string, int> entry in report.APBanks)
                banks.Add(entry.Key);
            foreach (KeyValuePair<string, int> entry in report.BPBanks) {
                if (!banks.Contains(entry.Key))
                    banks.Add(entry.Key);
            }
            foreach (KeyValuePair<string, int> entry in report.ABPBanks)
            {
                if (!banks.Contains(entry.Key))
                    banks.Add(entry.Key);
            }
            foreach (KeyValuePair<string, int> entry in report.OPBanks)
            {
                if (!banks.Contains(entry.Key))
                    banks.Add(entry.Key);
            }
            foreach (KeyValuePair<string, int> entry in report.ANBanks)
            {
                if (!banks.Contains(entry.Key))
                    banks.Add(entry.Key);
            }
            foreach (KeyValuePair<string, int> entry in report.BNBanks)
            {
                if (!banks.Contains(entry.Key))
                    banks.Add(entry.Key);
            }
            foreach (KeyValuePair<string, int> entry in report.ABNBanks)
            {
                if (!banks.Contains(entry.Key))
                    banks.Add(entry.Key);
            }
            foreach (KeyValuePair<string, int> entry in report.ONBanks)
            {
                if (!banks.Contains(entry.Key))
                    banks.Add(entry.Key);
            }
            return banks;
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
                            
                            #container {
                                height: 400px;
                            }

                            .highcharts-figure,
                            .highcharts-data-table table {
                                min-width: 310px;
                                max-width: 800px;
                                margin: 1em auto;
                            }

                            .highcharts-data-table table {
                                font-family: Verdana, sans-serif;
                                border-collapse: collapse;
                                border: 1px solid #ebebeb;
                                margin: 10px auto;
                                text-align: center;
                                width: 100%;
                                max-width: 500px;
                            }

                            .highcharts-data-table caption {
                                padding: 1em 0;
                                font-size: 1.2em;
                                color: #555;
                            }

                            .highcharts-data-table th {
                                font-weight: 600;
                                padding: 0.5em;
                            }

                            .highcharts-data-table td,
                            .highcharts-data-table th,
                            .highcharts-data-table caption {
                                padding: 0.5em;
                            }

                            .highcharts-data-table thead tr,
                            .highcharts-data-table tr:nth-child(even) {
                                background: #f8f8f8;
                            }

                            .highcharts-data-table tr:hover {
                                background: #f1f7ff;
                            }
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
