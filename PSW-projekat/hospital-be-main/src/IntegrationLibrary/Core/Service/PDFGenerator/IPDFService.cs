using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Service.PDFGenerator
{
    public interface IPDFService
    {
        void CreatePDF();
        String createFileName(String name);
        void CreatePDFStyle();

        void CreatePDFBody();
    }
}
