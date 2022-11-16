import { Component, Injectable, OnInit } from '@angular/core';
import { AbstractControl, AsyncValidator, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AllergiesAndDoctorsForPatientRegistrationDto } from '../model/allergiesAndDoctorsForPatientRegistrationDto.model';
import { Allergy } from '../model/allergy.model';
import { DoctorForPatientRegistrationDto } from '../model/doctorForPatientRegistrationDto.model';
import { RegisterPatientDto, Gender, BloodType } from '../model/registerPatientDto.model';
import { RegisterService } from '../services/register.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  public allergiesAndDoctors: AllergiesAndDoctorsForPatientRegistrationDto = new AllergiesAndDoctorsForPatientRegistrationDto();
  public registerForm: FormGroup | any;
  public usernameIsTaken:boolean = false;

  constructor(private registerService: RegisterService, private router: Router, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.registerService.getAllergiesAndDoctors().subscribe(res => {
      this.allergiesAndDoctors = res;
    })
    this.registerForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(30)]],
      surname: ['', Validators.required],
      gender: [Gender, Validators.required],
      birthDate: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      street: ['', Validators.required],
      number: ['', Validators.required],
      city: ['', Validators.required],
      township: ['', Validators.required],
      postCode: ['', Validators.required],
      username: ['', [Validators.required]],
      password: ['', Validators.required],
      bloodType: [BloodType, Validators.required],
      allergies: [Array<Allergy>],
      doctorName: [DoctorForPatientRegistrationDto, [Validators.required]]
    });
  }

  register(){
    let registerPatientDto: RegisterPatientDto = new RegisterPatientDto();
    registerPatientDto.name = this.registerForm.value.name;
    registerPatientDto.surname = this.registerForm.value.surname;
    registerPatientDto.gender = parseInt(this.registerForm.value.gender);
    registerPatientDto.birthDate = this.registerForm.value.birthDate;
    registerPatientDto.email = this.registerForm.value.email;
    registerPatientDto.street = this.registerForm.value.street;
    registerPatientDto.number = this.registerForm.value.number;
    registerPatientDto.city = this.registerForm.value.city;
    registerPatientDto.township = this.registerForm.value.township;
    registerPatientDto.postCode = this.registerForm.value.postCode;
    registerPatientDto.username = this.registerForm.value.username;
    registerPatientDto.password = this.registerForm.value.password;
    registerPatientDto.bloodType = parseInt(this.registerForm.value.bloodType);
    registerPatientDto.allergies = this.registerForm.value.allergies;
    registerPatientDto.doctorName = this.registerForm.value.doctorName;

    this.registerService.registerPatient(registerPatientDto).subscribe(res => {
      this.router.navigate(['/login']);
    },
    (err) => {
      if(err.error == "Username is taken.")
        this.usernameIsTaken = true
    });
  }
}
