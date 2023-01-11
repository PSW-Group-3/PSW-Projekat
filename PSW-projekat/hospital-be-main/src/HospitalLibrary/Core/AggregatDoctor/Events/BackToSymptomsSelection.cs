using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.AggregatDoctor.Events
{
    public class BackToSymptomsSelection : DomainEvent
    {
        public BackToSymptomsSelection()
        {
            phase = ExaminationStage.backToSymptoms;
            selectionTime = DateTime.Now;
        }
    }
}
