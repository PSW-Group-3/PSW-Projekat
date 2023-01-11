export class AppointmentSchedulingEventDTO{
    id: number;
    selectedItem: string;

    public constructor(id: number, selectedItem: string) {
        this.id = id;
        this.selectedItem = selectedItem;
    }
}