﻿using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public interface IMealService : IService<Meal>
    {
        IEnumerable<Meal> GetAllForPatientByDate(int patientId, DateTime dateTime);
        Meal GetByDateAndTypeForPatient(DateTime today, MealType mealType, int PatientId);
    }
}
