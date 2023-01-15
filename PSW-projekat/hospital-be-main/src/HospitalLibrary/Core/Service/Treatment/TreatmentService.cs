using System.Collections.Generic;
using System.IO;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;

namespace HospitalLibrary.Core.Service
{
    public class TreatmentService : ITreatmentService
    {
        private readonly ITreatmentRepository _treatmentRepository;

        public TreatmentService(ITreatmentRepository treatmentRepository)
        {
            _treatmentRepository = treatmentRepository;

        }
        
        public void Create(Treatment treatment)
        {
            treatment.Deleted = false;
            treatment.TreatmentState.Equals("open");
            _treatmentRepository.Create(treatment);
        }
        
        public void Delete(Treatment treatment)
        {
            treatment.Deleted = true;
            _treatmentRepository.Delete(treatment);
        }

        public IEnumerable<Treatment> GetAll()
        {
            return _treatmentRepository.GetAll();
        }

        public Treatment GetById(int id)
        {
            return _treatmentRepository.GetById(id);
        }

        public void Update(Treatment treatment)
        {
            treatment.Deleted = false;
           // treatment.TreatmentState.Equals("close");
            

            _treatmentRepository.Update(treatment);
        }

        public byte[] GeneratePdf(Treatment treatment)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);

                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                writer.Open();

                document.Open();

                Paragraph para5 = new Paragraph("GoCare", new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.ITALIC));
                Paragraph para6 = new Paragraph("Ive Andrica 10, Novi Sad", new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.ITALIC));
                Paragraph para7 = new Paragraph("hospital@gmail.com", new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.ITALIC));
                Paragraph para8 = new Paragraph("021/123-123", new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.ITALIC));
                para5.Alignment = Element.ALIGN_RIGHT;
                para6.Alignment = Element.ALIGN_RIGHT;
                para7.Alignment = Element.ALIGN_RIGHT;
                para8.Alignment = Element.ALIGN_RIGHT;
                document.Add(para5);
                document.Add(para6);
                document.Add(para7);
                document.Add(para8);

                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                document.Add(p);

                Paragraph para1 = new Paragraph("Discharge summary", new Font(Font.FontFamily.TIMES_ROMAN, 25, Font.BOLD));
                para1.Alignment = Element.ALIGN_CENTER;
                para1.SpacingAfter = 50;
                document.Add(para1);

                Paragraph para2 = new Paragraph("Patient name: " + treatment.Patient.Person.Name,
                                                new Font(Font.FontFamily.TIMES_ROMAN, 15));
                Paragraph para3 = new Paragraph("Patient surname: " + treatment.Patient.Person.Surname,
                                                new Font(Font.FontFamily.TIMES_ROMAN, 15));
                Paragraph para4 = new Paragraph("Room: " + treatment.Room.Number.Name,
                                                new Font(Font.FontFamily.TIMES_ROMAN, 15));
                Paragraph para52 = new Paragraph("Admission date: " + treatment.DateAdmission,
                                                new Font(Font.FontFamily.TIMES_ROMAN, 15));
                Paragraph para62 = new Paragraph("Reason for admission: " + treatment.ReasonForAdmission,
                                                new Font(Font.FontFamily.TIMES_ROMAN, 15));
                Paragraph para72 = new Paragraph("Discharge date: " + treatment.DateDischarge,
                                                new Font(Font.FontFamily.TIMES_ROMAN, 15));
                Paragraph para82 = new Paragraph("Reason for discharge: " + treatment.ReasonForDischarge,
                                                new Font(Font.FontFamily.TIMES_ROMAN, 15));


                
                para2.Alignment = Element.ALIGN_LEFT;
                para2.SpacingAfter = 10;
                para3.Alignment = Element.ALIGN_LEFT;
                para3.SpacingAfter = 10;
                para4.Alignment = Element.ALIGN_LEFT;
                para4.SpacingAfter = 10;
                para52.Alignment = Element.ALIGN_LEFT;
                para52.SpacingAfter = 10;
                para62.Alignment = Element.ALIGN_LEFT;
                para62.SpacingAfter = 10;
                para72.Alignment = Element.ALIGN_LEFT;
                para72.SpacingAfter = 10;
                para82.Alignment = Element.ALIGN_LEFT;
                para82.SpacingAfter = 10;

                document.Add(para2);
                document.Add(para3);
                document.Add(para4);
                document.Add(para52);
                document.Add(para62);
                document.Add(para72);
                document.Add(para82);

                Paragraph potpis = new Paragraph("Signature of the doctor:", new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD));
                potpis.SpacingBefore = 25;
                potpis.Alignment = Element.ALIGN_RIGHT;
                document.Add(potpis);

                Paragraph linija = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 25.0F, BaseColor.BLACK, Element.ALIGN_RIGHT, 1)));
                linija.SpacingBefore = 10;
                document.Add(linija);


                document.Close();
                writer.Close();
                var constant = ms.ToArray();

                return constant;


            }
        }
    }
}
