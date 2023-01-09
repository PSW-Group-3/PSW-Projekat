using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegrationLibrary.Core.Model;
using System.IO;
using IronPdf;

namespace IntegrationLibrary.Core.Service.Generators
{
    public class BloodReportPDFGenerator
    {
        String html;
        public async Task<byte[]> CreatePDF(List<BloodRequest> acceptedRequests, BloodBank bank)
        {
            var Renderer = new ChromePdfRenderer();

            CreatePDFStyle();
            CreatePDFBody(acceptedRequests, bank);
            
            PdfDocument doc = Renderer.RenderHtmlAsPdf(html);

            String filename = createFileName(bank.Name);
            doc.SaveAs(filename);

            string path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + @"\IntegrationAPI\" + filename;
            byte[] pdfFile = File.ReadAllBytes(path);
            return pdfFile;
        }
        public String createFileName(String bankName)
        {
            String today = DateTime.Now.ToString("ddMMyyyy");
            String filename = "BloodReportFor" + bankName.Replace(" ", "") + "_" + today + ".pdf";
            return filename;
        }

        public void DeleteMadeFiles(String bankName)
        {
            String path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + @"\IntegrationAPI\" + createFileName(bankName);
            if (File.Exists(path))
                File.Delete(path);
        }
        private void CreatePDFStyle()
        {
            html = @"<head><style>
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
                        </style></head>";
        }

        private void CreatePDFBody(List<BloodRequest> acceptedRequests, BloodBank bank)
        {
            html += "<body><h1>Blood usage report</h1><div>" +
                "<p>Blood bank: " + bank.Name + "</p>" +
                "<p>Bank email: " + bank.Email.EmailAddress + "</p><table class=\"GeneratedTable\"><thead><tr>" +
                                     " <th> Request Date </th>" +
                                     " <th> Blood type </th> " +
                                    "  <th> Quantity (units)</th></tr></thead><tbody>";

            foreach (BloodRequest request in acceptedRequests)
            {
                String requestDate = request.RequiredForDate.ToString("dd.MM.yyyy.");
                String bloodType = GetBloodTypeAsString(request.BloodType);
                html += "<tr><td>" + requestDate + "</td><td>" + bloodType + "</td><td>" + request.BloodQuantity + "</td></tr>";

            }


            html += @"</tbody></table></body>";
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
