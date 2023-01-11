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


                Paragraph para1 = new Paragraph("Discharge summary", new Font(Font.FontFamily.TIMES_ROMAN, 35, Font.BOLD));
                Paragraph para2 = new Paragraph("Patient name: " + treatment.Patient.Person.Name,
                                                new Font(Font.FontFamily.TIMES_ROMAN, 15));
                Paragraph para3 = new Paragraph("Patient surname: " + treatment.Patient.Person.Surname,
                                                new Font(Font.FontFamily.TIMES_ROMAN, 15));
                Paragraph para4 = new Paragraph("Room: " + treatment.Room.Number,
                                                new Font(Font.FontFamily.TIMES_ROMAN, 15));
                Paragraph para5 = new Paragraph("Admission date: " + treatment.DateAdmission,
                                                new Font(Font.FontFamily.TIMES_ROMAN, 15));
                Paragraph para6 = new Paragraph("Reason for admission: " + treatment.ReasonForAdmission,
                                                new Font(Font.FontFamily.TIMES_ROMAN, 15));
                Paragraph para7 = new Paragraph("Discharge date: " + treatment.DateDischarge,
                                                new Font(Font.FontFamily.TIMES_ROMAN, 15));
                Paragraph para8 = new Paragraph("Reason for discharge: " + treatment.ReasonForDischarge,
                                                new Font(Font.FontFamily.TIMES_ROMAN, 15));


                
                para1.Alignment = Element.ALIGN_CENTER;
                para1.SpacingAfter = 50;
                para2.Alignment = Element.ALIGN_LEFT;
                para2.SpacingAfter = 10;
                para3.Alignment = Element.ALIGN_LEFT;
                para3.SpacingAfter = 10;
                para4.Alignment = Element.ALIGN_LEFT;
                para4.SpacingAfter = 10;
                para5.Alignment = Element.ALIGN_LEFT;
                para5.SpacingAfter = 10;
                para6.Alignment = Element.ALIGN_LEFT;
                para6.SpacingAfter = 10;
                para7.Alignment = Element.ALIGN_LEFT;
                para7.SpacingAfter = 10;
                para8.Alignment = Element.ALIGN_LEFT;
                para8.SpacingAfter = 10;

                document.Add(para1);
                document.Add(para2);
                document.Add(para3);
                document.Add(para4);
                document.Add(para5);
                document.Add(para6);
                document.Add(para7);
                document.Add(para8);


                document.Close();
                writer.Close();
                var constant = ms.ToArray();

                return constant;


            }
        }
    }
}
