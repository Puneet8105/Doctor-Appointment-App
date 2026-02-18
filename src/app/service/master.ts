import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';


export interface GetAllPatient {
  patientId: number;
  patientName: string;
  mobileNo: string;
  city: string;
  email: string;
  address: string;
}




@Injectable({
  providedIn: 'root',
})




export class Master {

  constructor (private http:HttpClient){}

  createNewAppointment(obje:any){
    return this.http.post("https://localhost:7002/api/Appointment/CreateNewAppointment",obje)
  }

  getAllPatient(){
    return this.http.get<GetAllPatient[]>("https://localhost:7002/api/Appointment/GetAllPatient")
  }

  getAllAppointments() {
  return this.http.get<any[]>("https://localhost:7002/api/Appointment/GetAllAppointment");
}

getNewAppointments() {
  return this.http.get<any[]>("https://localhost:7002/api/Appointment/GetNewAppointmnt");
}

getDoneAppointments() {
  return this.http.get<any[]>("https://localhost:7002/api/Appointment/GetDoneAppointment");
}

changeStatus(id: number) {
  return this.http.put<{message : string}>(
    `https://localhost:7002/api/Appointment/ChangeStatus/${id}`, {}
  );
}

  
}
