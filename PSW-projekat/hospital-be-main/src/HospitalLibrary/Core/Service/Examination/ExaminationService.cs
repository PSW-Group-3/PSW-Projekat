using System;
using System.Collections.Generic;
using System.IO;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;

namespace HospitalLibrary.Core.Service
{
    public class ExaminationService : IExaminationService
    {
        private readonly IExaminationRepository _examinationRepository;
        private readonly ISymptomRepository _symptomRepository;
        private readonly IMedicineRepository _medicineRepository;


        public ExaminationService(IExaminationRepository examinationRepository, ISymptomRepository symptomRepository, IMedicineRepository medicineRepository)
        {
            _examinationRepository = examinationRepository;
            _symptomRepository = symptomRepository;
            _medicineRepository = medicineRepository;
        }

        public void Create(Examination entity)
        {
            entity.Deleted = false;
            _examinationRepository.Create(entity);
        }

        public void Delete(Examination entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Examination> GetAll()
        {
            return _examinationRepository.GetAll();
        }

        public Examination GetById(int id)
        {
            return _examinationRepository.GetById(id);
        }

        public void Update(Examination entity)
        {
            throw new NotImplementedException();
        }

        public List<Symptom> GetHelpSymptoms(Examination examination)
        {
            List<Symptom> pomocniSimptomi = new List<Symptom>();
            foreach (Symptom simptom in examination.Symptoms)
            {
                Symptom pomocniSimptom = new Symptom();
                pomocniSimptom = _symptomRepository.GetById(simptom.Id);
                pomocniSimptomi.Add(pomocniSimptom);
            }
            return pomocniSimptomi;
        }

        public List<Medicine> GetHelpMedicines(Prescription prescription)
        {
            List<Medicine> pomocniLekovi = new List<Medicine>();
            foreach (Medicine medicine in prescription.Medicines)
            {
                Medicine pomocniLek = new Medicine();
                pomocniLek = _medicineRepository.GetById(medicine.Id);
                pomocniLekovi.Add(pomocniLek);
            }
            return pomocniLekovi;
        }

        public byte[] GeneratePdf(Examination examination, Boolean symptoms, Boolean report, Boolean medications)
        {
            using (MemoryStream ms = new MemoryStream())
            {

                Document document = new Document(PageSize.A4, 25, 25, 30, 30);

                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                writer.Open();

                document.Open();

                //Image logo = Image.GetInstance("https://e7.pngegg.com/pngimages/811/814/png-clipart-white-hospital-illustration-children-s-hospital-free-content-hopital-presentation-sticker-thumbnail.png");
                //logo.ScaleAbsolute(40, 40);
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

                Paragraph para1 = new Paragraph("Examination summary", new Font(Font.FontFamily.TIMES_ROMAN, 25, Font.BOLD));
                para1.Alignment = Element.ALIGN_CENTER;
                para1.SpacingAfter = 50;
                document.Add(para1);

                if (symptoms)
                {
                    document.Add(new Paragraph("Symptoms: ", new Font(Font.FontFamily.TIMES_ROMAN, 15, Font.BOLD)));
                    foreach (Symptom symptom in examination.Symptoms)
                    {
                        Console.WriteLine(symptom.Name);
                        document.Add(new Paragraph(symptom.Name, new Font(Font.FontFamily.TIMES_ROMAN, 15)));
                    }
                }

                Paragraph para3 = new Paragraph();
                para3.SpacingAfter = 15;
                document.Add(para3);

                if (report)
                {
                    document.Add(new Paragraph("Report: ", new Font(Font.FontFamily.TIMES_ROMAN, 15, Font.BOLD)));
                    document.Add(new Paragraph(examination.Report, new Font(Font.FontFamily.TIMES_ROMAN, 15)));
                }

                Paragraph para4 = new Paragraph();
                para4.SpacingAfter = 15;
                document.Add(para4);

                if (medications)
                {
                    document.Add(new Paragraph("Medicines: ", new Font(Font.FontFamily.TIMES_ROMAN, 15, Font.BOLD)));
                    foreach (Prescription prescription in examination.Prescriptions)
                    {
                        foreach (Medicine medicine in prescription.Medicines)
                        {
                            Console.WriteLine(medicine.Name);
                            document.Add(new Paragraph(medicine.Name, new Font(Font.FontFamily.TIMES_ROMAN, 15)));
                            document.Add(new Paragraph(prescription.Description, new Font(Font.FontFamily.TIMES_ROMAN, 15, Font.ITALIC)));
                            Paragraph paraaaa = new Paragraph("");
                            paraaaa.SpacingAfter = 7;
                            document.Add(paraaaa);
                        }
                    }
                }

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

        public List<Examination> GetAllExaminationsByDoctor(int personId)
        {
            return _examinationRepository.GetAllExaminationsByDoctor(personId);
        }

        public string getBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.IndexOf(strStart) > strSource.IndexOf(strEnd)) { return ""; }

            if (strSource.ToLower().Contains(strStart.ToLower()) && strSource.ToLower().Contains(strEnd.ToLower()))
            {
                int Start, End;
                Start = strSource.ToLower().IndexOf(strStart.ToLower(), 0) + strStart.Length;
                End = strSource.ToLower().IndexOf(strEnd.ToLower(), Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }

        public List<Examination> GetAllExaminationsBySearchReport(string searchText, int personId)
        {
            List<Examination> examinations = new List<Examination>();

            //Pretraga sadrzaja po frazama
            if (searchText.Contains("'"))
            {
                searchText = searchText.Replace("'", string.Empty);
                string[] splittedSearchText = searchText.Split(' ');

                foreach (Examination examination in GetAllExaminationsByDoctor(personId))
                {
                    string konacno = "";
                    string cleanAmount = examination.Report.Replace(".", string.Empty);
                    string[] splittedReport = cleanAmount.Split(' ');
                    string lastElement = splittedSearchText[splittedSearchText.Length - 1];
                    string firstElement = splittedSearchText[0];

                    if (splittedSearchText.Length > 1)
                    {
                        string izmedju = getBetween(cleanAmount, firstElement, lastElement);
                        string izmedjuTrimovan = izmedju.Trim();

                        if (izmedjuTrimovan.Equals("") && !izmedju.Equals("")) { konacno = firstElement + " " + lastElement; }
                        else if (!izmedjuTrimovan.Equals("") && !izmedju.Equals("")) { konacno = firstElement + " " + izmedjuTrimovan + " " + lastElement; }

                        if (konacno.ToLower().Equals(searchText.ToLower())) { examinations.Add(examination); }
                    }

                    if (splittedSearchText.Length == 1)
                    {
                        foreach (string s1 in splittedReport)
                        {
                            if (s1.ToLower().Equals(searchText.ToLower()))
                                examinations.Add(examination);
                        }
                    }

                }
            }

            else
            {
                foreach (Examination examination in GetAllExaminationsByDoctor(personId))
                {
                    string[] splittedSearchText2 = searchText.Split(' ');

                    foreach (string s1 in splittedSearchText2)
                    {
                        if (examination.Report.ToLower().Contains(s1.ToLower())) { examinations.Add(examination); }
                    }
                }
            }

            return examinations;
        }

        public List<Examination> GetAllExaminationsBySearchPrescription(string searchText, int personId)
        {

            List<Examination> examinations = new List<Examination>();

            if (searchText.Contains("'"))
            {
                searchText = searchText.Replace("'", string.Empty);
                string[] splittedSearchText = searchText.Split(' ');

                foreach (Examination examination in GetAllExaminationsByDoctor(personId))
                {
                    foreach (Prescription prescription in examination.Prescriptions)
                    {
                        string konacno = "";
                        string cleanAmount = prescription.Description.Replace(".", string.Empty);
                        string[] splittedPrescription = cleanAmount.Split(' ');
                        string lastElement = splittedSearchText[splittedSearchText.Length - 1];
                        string firstElement = splittedSearchText[0];

                        if (splittedSearchText.Length > 1)
                        {
                            string izmedju = getBetween(cleanAmount, firstElement, lastElement);
                            string izmedjuTrimovan = izmedju.Trim();

                            if (izmedjuTrimovan.Equals("") && !izmedju.Equals("")) { konacno = firstElement + " " + lastElement; }
                            else if (!izmedjuTrimovan.Equals("") && !izmedju.Equals("")) { konacno = firstElement + " " + izmedjuTrimovan + " " + lastElement; }

                            if (konacno.ToLower().Equals(searchText.ToLower())) { examinations.Add(examination); }
                        }

                        if (splittedSearchText.Length == 1)
                        {
                            foreach (string s1 in splittedPrescription)
                            {
                                if (s1.ToLower().Equals(searchText.ToLower()))
                                    examinations.Add(examination);
                            }
                        }
                    }

                }
            }

            else
            {
                foreach (Examination examination in GetAllExaminationsByDoctor(personId))
                {
                    foreach (Prescription prescription in examination.Prescriptions)
                    {
                        string[] splittedSearchText2 = searchText.Split(' ');

                        foreach (string s1 in splittedSearchText2)
                        {
                            if (prescription.Description.ToLower().Contains(s1.ToLower())) { examinations.Add(examination); }
                        }
                    }
                  
                }
            }

            return examinations;
        }

    }
}
