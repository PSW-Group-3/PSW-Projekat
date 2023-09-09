using HospitalAPI.DTO;
using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;

namespace HospitalAPI.Adapters
{
    public class WorkoutAdapter
    {
        public static Exercise FromExerciseDTOtoExercise(ExerciseDTO dto)
        {
            return new Exercise(dto.Name, dto.Description, dto.Sets, dto.Reps, dto.WeightLifted);
        }

        public static List<Exercise> FromExerciseDTOListToExerciseList(List<ExerciseDTO> dtos)
        {
            List<Exercise> exercises = new();

            foreach(ExerciseDTO dto in dtos)
            {
                exercises.Add(FromExerciseDTOtoExercise(dto));
            }

            return exercises;
        }

        public static GymWorkout FromGymWorkoutDTOtoGymWorkout(GymWorkoutDTO dto, Patient patient)
        {
            return new(dto.Type, DateTime.Today, TimeSpan.FromMinutes(dto.Duration), dto.Description, patient, FromExerciseDTOListToExerciseList(dto.Exercises));
        }

        public static Workout FromWorkoutDTOtoWorkout(WorkoutDTO dto, Patient patient)
        {
            return new(dto.Type, DateTime.Today, TimeSpan.FromMinutes(dto.Duration), dto.Description, patient);
        }
    }
}
