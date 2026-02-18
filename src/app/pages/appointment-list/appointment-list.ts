import { Component } from '@angular/core';
import { Master } from '../../service/master';
import {DatePipe, UpperCasePipe } from '@angular/common';


@Component({
  selector: 'app-appointment-list',
  standalone:true,
  imports: [DatePipe,UpperCasePipe],
  templateUrl: './appointment-list.html',
  styleUrl: './appointment-list.css',
})
export class AppointmentList {

    public title = "Appointments";

  appointments: any[] = [];
filterType: string = 'all';

constructor(private mastServ: Master) {}

ngOnInit() {
  this.loadAppointments('new');
}

loadAppointments(type: string) {
  this.filterType = type;

  if (type === 'all') {
    this.mastServ.getAllAppointments().subscribe(res => this.appointments = res);
  }
  else if (type === 'new') {
    this.mastServ.getNewAppointments().subscribe(res => this.appointments = res);
  }
  else if (type === 'done') {
    this.mastServ.getDoneAppointments().subscribe(res => this.appointments = res);
  }
}






markDone(id: number) {
  this.mastServ.changeStatus(id).subscribe({
    next: (res) => {
      alert(res.message);                 
      this.loadAppointments(this.filterType);  
    },
    error: (err) => {
      alert(err.error?.message || "Update failed");
    }
  });
}


}
