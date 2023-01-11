import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDatepickerInput } from '@angular/material/datepicker';
import { ToastrService } from 'ngx-toastr';
import { ReportSettings } from '../model/report-settings';
import { ReportSettingsService } from '../services/report-settings.service';

@Component({
  selector: 'app-report-settings',
  templateUrl: './report-settings.component.html',
  styleUrls: ['./report-settings.component.css']
})
export class ReportSettingsComponent implements OnInit {

  public selectedOptionDelivery: any = 0;
  public selectedOptionCalculation: any = 0;
  public settings: ReportSettings;
  public errorMessage: any;

  constructor(private reportSettingsService: ReportSettingsService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.reportSettingsService.getReportSettings().subscribe(res => {
      this.settings = res;
      this.setCalculationSelect();
      this.setDeliverySelect();
    })
    
  }
  
  updateSettings() : void {
    this.setCalculationResult();
    this.setDeliveryResult();
    this.reportSettingsService.updateSettings(this.settings).subscribe(res => {
      this.toastr.success("Successfully changed settings");
    }, (error) => {
      this.errorMessage = error;
      this.toastError();
    });
    console.log(this.settings)
  }

  private setCalculationResult(): void {
    if(this.selectedOptionCalculation == 0){
      this.settings.calculationMonths = 1;
      this.settings.calculationDays = 0;
      this.settings.calculationYears = 0;
    } else if(this.selectedOptionCalculation == 1){
      this.settings.calculationMonths = 6;
      this.settings.calculationDays = 0;
      this.settings.calculationYears = 0;
    } else if(this.selectedOptionCalculation == 2){
      this.settings.calculationMonths = 0;
      this.settings.calculationDays = 0;
      this.settings.calculationYears = 1;
    }
  }

  private setDeliveryResult(): void {
    if(this.selectedOptionDelivery == 0){
      this.settings.deliveryMonths = 1;
      this.settings.deliveryDays = 0;
      this.settings.deliveryYears = 0;
    } else if(this.selectedOptionDelivery == 1){
      this.settings.deliveryMonths = 6;
      this.settings.deliveryDays = 0;
      this.settings.deliveryYears = 0;
    } else if(this.selectedOptionDelivery == 2){
      this.settings.deliveryMonths = 0;
      this.settings.deliveryDays = 0;
      this.settings.deliveryYears = 1;
    }
  }

  private setCalculationSelect(): void {
    if(this.settings.calculationMonths == 1 
      && this.settings.calculationDays == 0 
      && this.settings.calculationYears == 0){
        this.selectedOptionCalculation = "0";
    } else if(this.settings.calculationMonths == 6 
      && this.settings.calculationDays == 0 
      && this.settings.calculationYears == 0){
        this.selectedOptionCalculation = "1";
    } else if(this.settings.calculationMonths == 0 
      && this.settings.calculationDays == 0 
      && this.settings.calculationYears == 1){
        this.selectedOptionCalculation = "2";
    } else {
      this.selectedOptionCalculation = "3";
    }
  }

  private setDeliverySelect(): void {
    if(this.settings.deliveryMonths == 1 
      && this.settings.deliveryDays == 0 
      && this.settings.deliveryYears == 0){
        this.selectedOptionDelivery = "0";
    } else if(this.settings.deliveryMonths == 6 
      && this.settings.deliveryDays == 0 
      && this.settings.deliveryYears == 0){
        this.selectedOptionDelivery = "1";
    } else if(this.settings.deliveryMonths == 0 
      && this.settings.deliveryDays == 0 
      && this.settings.deliveryYears == 1){
        this.selectedOptionDelivery = "2";
    } else {
      this.selectedOptionDelivery = "3";
    }
  }

  private toastError() {
    this.toastr.error(this.errorMessage);
  }
}
