using IntegrationLibrary.Core.Model;
using IronPdf;
using System;
using System.Collections.Generic;
using System.IO;

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
            CreateStackChart(bloodTypesByBanks, banks, bloodAmount);
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

            html += "<table class=\"GeneratedTable\" ><thead><tr>" +
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
                                if (bloodTypesByBanks[bankIndex][index] > 0)
                                {
                                    if (!isFirst)
                                        html += "<br>";
                                    html += bank.Name + "(" + bloodTypesByBanks[bankIndex][index] + " units)";
                                    isFirst = false;
                                }

                                bankIndex++;
                            }

                            break;
                        case "B positive":
                            isFirst = true;
                            foreach (BloodBank bank in banks)
                            {
                                
                                if (bloodTypesByBanks[bankIndex][index] > 0)
                                {
                                    if (!isFirst)
                                        html += "<br>";
                                    html += bank.Name + "(" + bloodTypesByBanks[bankIndex][index] + " units)";
                                    isFirst = false;
                                }
                                bankIndex++;
                            }

                            break;
                        case "AB positive":
                            isFirst = true;
                            foreach (BloodBank bank in banks)
                            {
                                if (bloodTypesByBanks[bankIndex][index] > 0)
                                {
                                    if (!isFirst)
                                        html += "<br>";
                                    html += bank.Name + "(" + bloodTypesByBanks[bankIndex][index] + " units)";
                                    isFirst = false;
                                }
                                bankIndex++;
                            }

                            break;
                        case "O positive":
                            isFirst = true;
                            foreach (BloodBank bank in banks)
                            {
                                if (bloodTypesByBanks[bankIndex][index] > 0)
                                {
                                    if (!isFirst)
                                        html += "<br>";
                                    html += bank.Name + "(" + bloodTypesByBanks[bankIndex][index] + " units)";
                                    isFirst = false;
                                }
                                bankIndex++;
                            }

                            break;
                        case "A negative":
                            isFirst = true;
                            foreach (BloodBank bank in banks)
                            {
                                if (bloodTypesByBanks[bankIndex][index] > 0)
                                {
                                    if (!isFirst)
                                        html += "<br>";
                                    html += bank.Name + "(" + bloodTypesByBanks[bankIndex][index] + " units)";
                                    isFirst = false;
                                }
                                bankIndex++;
                            }

                            break;
                        case "B negative":
                            isFirst = true;
                            foreach (BloodBank bank in banks)
                            {
                                if (bloodTypesByBanks[bankIndex][index] > 0)
                                {
                                    if (!isFirst)
                                        html += "<br>";
                                    html += bank.Name + "(" + bloodTypesByBanks[bankIndex][index] + " units)";
                                    isFirst = false;
                                }
                                bankIndex++;
                            }

                            break;
                        case "AB negative":
                            isFirst = true;
                            foreach (BloodBank bank in banks)
                            {
                                if (bloodTypesByBanks[bankIndex][index] > 0)
                                {
                                    if (!isFirst)
                                        html += "<br>";
                                    html += bank.Name + "(" + bloodTypesByBanks[bankIndex][index] + " units)";
                                    isFirst = false;
                                }
                                bankIndex++;
                            }

                            break;
                        case "O negative":
                            isFirst = true;
                            foreach (BloodBank bank in banks)
                            {
                                if (bloodTypesByBanks[bankIndex][index] > 0)
                                {
                                    if (!isFirst)
                                        html += "<br>";
                                    html += bank.Name + "(" + bloodTypesByBanks[bankIndex][index] + " units)";
                                    isFirst = false;
                                }
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
            html += "<div id=\"container\" style = \"height: 816px; width: 960px;\"></div>";

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
                    </script>";
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
        public void CreateStackChart(List<List<int>> bloodTypesByBanks, List<BloodBank> banks, List<int> bloodAmount)
        {
            html += @"<div id='chart' style='width: 950px;'></div>";
  

            html += "<br><br><br><figure class=\"highcharts-figure\"><div id =\"containeer\"></div></figure>";



            //html += "<div id=\"container\" style = \"height: 816px; width: 1056px; \"></div>";
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
            int index = 0;
            foreach (BloodBank bank in banks)
            {
                if (!isFirst)
                    html += ",";
                html += "{name: '" + bank.Name + "', data: [" + bloodTypesByBanks[index][0] + ", " +
                                        bloodTypesByBanks[index][1] + ", " +
                                        bloodTypesByBanks[index][2] + ", " +
                                        bloodTypesByBanks[index][3] + ", " +
                                        bloodTypesByBanks[index][4] + ", " +
                                        bloodTypesByBanks[index][5] + ", " +
                                        bloodTypesByBanks[index][6] + ", " +
                                        bloodTypesByBanks[index][7] + "]}";
                isFirst = false;
                index++;      
            }
            html += @"]
                      });
                    </script>

                    </body>
                </html>";

        }
        public void CreatePDFStyle()
        {
            html = @"<head>
                        <meta charset='utf-8' />
                        <title>Emergency Blood Report PDF</title>
                        <style>
                            table.GeneratedTable {
                            width: 100%; 
                            background-color: #ffffff; 
                            border-collapse: collapse;
                            border-width: 2px; 
                            border-color: #2596be; 
                            border-style: solid; 
                            color: #000000;
                            margin-bottom: 70px;} 
                        table.GeneratedTable td, table.GeneratedTable th { 
                            border-width: 2px; 
                            border-color: #2596be; 
                            border-style: solid; 
                            padding: 3px;}
                        table.GeneratedTable thead { 
                            background-color: #2596be; }

                        table.GeneratedTable tr:nth-child(even) {
                            background-color: rgb(121, 117, 117);
                            color: white;
                        }
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
    }
}
