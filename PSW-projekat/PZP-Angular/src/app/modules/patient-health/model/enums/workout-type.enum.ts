export enum WorkoutType {
  walking,
  jogging,
  running,
  cardio,
  cycling,
  swimming,
  strength,
  sports,
  undefined,
}

export function getWorkoutTypeString(type: WorkoutType): string {
  switch (type) {
    case WorkoutType.walking:
      return 'Walking';
    case WorkoutType.jogging:
      return 'Jogging';
    case WorkoutType.running:
      return 'Running';
    case WorkoutType.cardio:
      return 'Cardio';
    case WorkoutType.cycling:
      return 'Cycling';
    case WorkoutType.swimming:
      return 'Swimming';
    case WorkoutType.strength:
      return 'Strength';
    case WorkoutType.sports:
      return 'Sports';
    case WorkoutType.undefined:
      return 'You did not workout today';
    default:
      return 'Unknown';
  }
}
