export enum MealType {
    breakfast,
    lunch,
    dinner,
    water,
    undefined
  }
  
  export function GetMealTypeString(type: MealType): string {
    switch (type) {
      case MealType.breakfast:
        return 'Breakfast';
      case MealType.lunch:
        return 'Lunch';
      case MealType.dinner:
        return 'Dinner';
      case MealType.water:
        return 'Water intake';
      case MealType.undefined:
        return 'You did not add any meals today';
      default:
        return 'Unknown';
    }
  }