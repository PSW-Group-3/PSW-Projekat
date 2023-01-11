import { Pipe, PipeTransform } from '@angular/core';
import { Medicine } from '../model/medicine';
import { Prescription } from '../model/prescription';

@Pipe({
  name: 'showMedicines'
})
export class ShowMedicinesPipe implements PipeTransform {

  
  transform(medicine: Prescription[]): String {
    let toRet = "";
    medicine.forEach((element, index) => {
      element.medicines.forEach((element1) => {
        toRet += element1.name + " ";
      })
    })
    return toRet;
  }
  
  

}
