import { Component } from '@angular/core';
import { Master } from '../../service/master';
import { GetAllPatient } from '../../service/master';
import { FormsModule } from "@angular/forms";

@Component({
  selector: 'app-patient-list',
  imports: [FormsModule],
  templateUrl: './patient-list.html',
  styleUrl: './patient-list.css',
})


export class PatientList {

  searchText: string = '';
  filteredPatients: any[] = [];

  patients: GetAllPatient[] = [];

  constructor(private mastServ: Master) { }


  ngOnInit() {
    this.loadpatients();
  
  }

  searchPatients() {
  const text = this.searchText?.toLowerCase() || '';

  this.filteredPatients = this.patients.filter(p =>
    p.patientName?.toLowerCase().includes(text) ||
    p.city?.toLowerCase().includes(text) ||
    p.mobileNo?.includes(text) ||
    p.address?.includes(text)
  );
}


  loadpatients() {
    this.mastServ.getAllPatient().subscribe((res: GetAllPatient[]) => {
      this.patients = res;
      this.filteredPatients = res;
    });
  }
}
