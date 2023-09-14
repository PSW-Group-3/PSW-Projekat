using AutoMapper;
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
        public static ExerciseDTO FromExerciseToExerciseDTO(Exercise exercise)
        {
            return new ExerciseDTO(exercise.Name, exercise.Description, exercise.Sets, exercise.Reps, exercise.WeightLifted);
        }
        public static List<ExerciseDTO> FromExerciseListToExerciseDTOList(List<Exercise> exercises)
        {
            List<ExerciseDTO> dtos = new();

            foreach (Exercise e in exercises)
            {
                dtos.Add(FromExerciseToExerciseDTO(e));
            }

            return dtos;
        }

        public static GymWorkout FromAddGymWorkoutDTOtoGymWorkout(AddGymWorkoutDTO dto, Patient patient)
        {
            return new(0, dto.Type, DateTime.Today, TimeSpan.FromMinutes(dto.Duration), dto.Description, patient, FromExerciseDTOListToExerciseList(dto.Exercises));
        }
        public static GymWorkoutInfoDTO FromGymWorkoutToGymWorkoutInfoDTO(GymWorkout workout)
        {
            return new(workout.Type, workout.Date, (int)workout.Duration.TotalMinutes, workout.Description, workout.Patient.Person.Id, FromExerciseListToExerciseDTOList(workout.Exercises));
        }
        public static List<GymWorkoutInfoDTO> FromGymWorkoutListToGymWorkoutInfoDTOList(List<GymWorkout> workouts)
        {
            List<GymWorkoutInfoDTO> dtos = new();

            foreach (GymWorkout w in workouts)
            {
                dtos.Add(FromGymWorkoutToGymWorkoutInfoDTO(w));
            }

            return dtos;
        }

        public static Workout FromAddWorkoutDTOtoWorkout(AddWorkoutDTO dto, double score, Patient patient)
        {
            return new(score, dto.Type, DateTime.Today, TimeSpan.FromMinutes(dto.Duration), dto.Description, patient);
        }
        public static WorkoutInfoDTO FromWorkoutToWorkoutInfoDTO(Workout workout)
        {
            return new(workout.Type, workout.Date, (int)workout.Duration.TotalMinutes, workout.Description, workout.Patient.Person.Id);
        }
        public static List<WorkoutInfoDTO> FromWorkoutListToWorkoutInfoDTOList(List<Workout> workouts)
        {
            List<WorkoutInfoDTO> dtos = new();

            foreach (Workout w in workouts)
            {
                dtos.Add(FromWorkoutToWorkoutInfoDTO(w));
            }

            return dtos;
        }
    }
}
