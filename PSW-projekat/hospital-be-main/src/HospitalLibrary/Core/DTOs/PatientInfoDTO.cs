using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DTOs
{
    public class PatientInfoDTO
    {
        public String FullName { get; set; }
        public double HealthScore { get; set; }
        public DateTime SelectedDate { get; set; }
        public float Weight { get; set; }
        public int Height { get; set; }
        public String BloodPressure { get; set; }
        public int HeartRate { get; set; }
    }
}
