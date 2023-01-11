using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.AggregatDoctor
{
    public enum ExaminationStage
    {
        beginning, symptomsChoosen, reortWritten, prescriptionsChoosen, backToSymptoms, backToReort, backToPrescription, eximinationFinished
    }
}
