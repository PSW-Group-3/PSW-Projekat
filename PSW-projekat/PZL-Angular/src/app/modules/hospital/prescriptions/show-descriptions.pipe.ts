import { Pipe, PipeTransform } from '@angular/core';
import { Prescription } from '../model/prescription';

@Pipe({
  name: 'showDescriptions'
})
export class ShowDescriptionsPipe implements PipeTransform {

  transform(descriptions: Prescription[]): String {
    let toRet = "";
    descriptions.forEach((element, index) => {
      if(index == descriptions.length - 1){
        toRet += element.description ;
      } else {
        toRet += element.description + ", ";
      }
    })
    return toRet;
  }

}
